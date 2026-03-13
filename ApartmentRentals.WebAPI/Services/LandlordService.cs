using ApartmentRentals.Data.Models;
using ApartmentRentals.Data.Repositories;
using ApartmentRentals.WebAPI.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;
using static ApartmentRentals.Data.Repositories.IRepository<ApartmentRentals.Data.Models.Landlord>;

namespace SpaceStoreApi.Services;

public class LandlordService: IRepository<Landlord>
{
    private readonly IMongoCollection<Landlord> _landlordsCollection;

    public LandlordService(IOptions<MongoDbContext> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        
        _landlordsCollection = database.GetCollection<Landlord>(
            settings.Value.LandlordsCollectionName);
    }

    public async Task<IEnumerable<Landlord>> GetAllAsync() =>
        await _landlordsCollection.Aggregate().Sample(SAMPLE_NUM).ToListAsync();

    public async Task<IEnumerable<Landlord>> GetFilteredByPropertyAsync(string propertyName, string value)
    {
        var property = typeof(Landlord).GetProperty(propertyName,
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

            return await _landlordsCollection.Find(Builders<Landlord>.Filter.Eq(property.Name, convertedValue)).ToListAsync();
        }
        catch (Exception)
        {
            throw new PropertyFilterException($"Неверный тип значения '{value}' для поля '{propertyName}'");
        }
    }

    public async Task<Landlord?> GetByIdAsync(string id) =>
        await _landlordsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Landlord landlord)
    {
        await _landlordsCollection.InsertOneAsync(landlord);
    }

    public async Task<bool> UpdateAsync(string id, Landlord landlord)
    {
        var result = await _landlordsCollection.ReplaceOneAsync(x => x.Id == id, landlord);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteByIdAsync(string id)
    {
        var result = await _landlordsCollection.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<bool> DeleteAllAsync()
    {
        var result = await _landlordsCollection.DeleteManyAsync(_ => true);

        return result.DeletedCount > 0;
    }
}