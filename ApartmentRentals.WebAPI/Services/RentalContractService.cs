using ApartmentRentals.Data.Models;
using ApartmentRentals.Data.Repositories;
using ApartmentRentals.WebAPI.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;
using static ApartmentRentals.Data.Repositories.IRepository<ApartmentRentals.Data.Models.RentalContract>;

namespace SpaceStoreApi.Services;

public class RentalContractService: IRepository<RentalContract>
{
    private readonly IMongoCollection<RentalContract> _rentalContractCollection;

    public RentalContractService(
        IOptions<MongoDbContext> rentalContractStoreDatabaseSettings)
    {
       
        var mongoClient = new MongoClient(
            rentalContractStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            rentalContractStoreDatabaseSettings.Value.DatabaseName);

        _rentalContractCollection = mongoDatabase.GetCollection<RentalContract>(
            rentalContractStoreDatabaseSettings.Value.RentalContractCollectionName);
    }

    public async Task<IEnumerable<RentalContract>> GetAllAsync() =>
        await _rentalContractCollection.Aggregate().Sample(SAMPLE_NUM).ToListAsync();

    public async Task<IEnumerable<RentalContract>> GetFilteredByPropertyAsync(string propertyName, string value)
    {
        var property = typeof(RentalContract).GetProperty(propertyName,
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

            return await _rentalContractCollection.Find(Builders<RentalContract>.Filter.Eq(property.Name, convertedValue)).ToListAsync();
        }
        catch (Exception)
        {
            throw new PropertyFilterException($"Неверный тип значения '{value}' для поля '{propertyName}'");
        }
    }

    public async Task<RentalContract?> GetByIdAsync(string id) =>
        await _rentalContractCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(RentalContract newContract) =>
        await _rentalContractCollection.InsertOneAsync(newContract);

    public async Task<bool> UpdateAsync(string id, RentalContract updatedContract) {
        //await _spaceCollection.ReplaceOneAsync(x => x.Id == /*int.Parse(id)*/ id, updatedSpace);
        var result = await _rentalContractCollection.ReplaceOneAsync(x => x.Id == id, updatedContract);
        // Возвращаем true, если документ был изменён (ModifiedCount > 0)
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteByIdAsync(string id) {
        //await _spaceCollection.DeleteOneAsync(x => x.Id == /*int.Parse(id)*/ id);
        var result = await _rentalContractCollection.DeleteOneAsync(x => x.Id == id);
        
        // Возвращаем true, если документ был удалён (DeletedCount > 0)
        return result.DeletedCount > 0;
    }

    public async Task<bool> DeleteAllAsync()
    {
        var result = await _rentalContractCollection.DeleteManyAsync(_ => true);

        return result.DeletedCount > 0;
    }
}