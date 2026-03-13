using ApartmentRentals.Data.Models;
using ApartmentRentals.Data.Repositories;
using ApartmentRentals.WebAPI.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;
using static ApartmentRentals.Data.Repositories.IRepository<ApartmentRentals.Data.Models.Space>;

namespace SpaceStoreApi.Services;

public class SpaceService : IRepository<Space>
{
    private readonly IMongoCollection<Space> _spaceCollection;

    public SpaceService(
        IOptions<MongoDbContext> spaceStoreDatabaseSettings)
    {
       
        var mongoClient = new MongoClient(
            spaceStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            spaceStoreDatabaseSettings.Value.DatabaseName);

        _spaceCollection = mongoDatabase.GetCollection<Space>(
            spaceStoreDatabaseSettings.Value.SpaceCollectionName);
    }

    public async Task<IEnumerable<Space>> GetAllAsync() =>
        await _spaceCollection.Aggregate().Sample(SAMPLE_NUM).ToListAsync();

    public async Task<IEnumerable<Space>> GetFilteredByPropertyAsync(string propertyName, string value)
    {
        var property = typeof(Space).GetProperty(propertyName,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (property == null)
            throw new PropertyFilterException($"Поле '{propertyName}' не найдено");

        try
        {
            object convertedValue;
            Type targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

            if (targetType.IsEnum)
            {
                convertedValue = Enum.Parse(targetType, value, true);
            }
            else
            {
                convertedValue = Convert.ChangeType(value, targetType);
            }

            return await _spaceCollection.Find(Builders<Space>.Filter.Eq(property.Name, convertedValue)).ToListAsync();
        }
        catch (Exception)
        {
            throw new PropertyFilterException($"Неверный тип значения '{value}' для поля '{propertyName}'");
        }
    }

    public async Task<Space?> GetByIdAsync(string id) =>
        await _spaceCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Space newSpace) {
        
        await _spaceCollection.InsertOneAsync(newSpace);
    }

    public async Task<bool> UpdateAsync(string id, Space updatedSpace) {
        updatedSpace.Id = id;
        var result = await _spaceCollection.ReplaceOneAsync(x => x.Id == id, updatedSpace);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteByIdAsync(string id) {
        var result = await _spaceCollection.DeleteOneAsync(x => x.Id == id);
        
        return result.DeletedCount > 0;
    }

    public async Task<bool> DeleteAllAsync()
    {
        var result = await _spaceCollection.DeleteManyAsync(_ => true);

        return result.DeletedCount > 0;
    }
}