using ApartmentRentals.Data.Models;
using ApartmentRentals.Data.Repositories;
using ApartmentRentals.WebAPI.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;
using static ApartmentRentals.Data.Repositories.IRepository<ApartmentRentals.Data.Models.Tenant>;

namespace SpaceStoreApi.Services;

public class TenantService : IRepository<Tenant>
{
    private readonly IMongoCollection<Tenant> _tenantCollection;

    public TenantService(
        IOptions<MongoDbContext> tenantStoreDatabaseSettings)
    {

        var mongoClient = new MongoClient(
            tenantStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            tenantStoreDatabaseSettings.Value.DatabaseName);

        _tenantCollection = mongoDatabase.GetCollection<Tenant>(
            tenantStoreDatabaseSettings.Value.TenantCollectionName);
    }

    public async Task<IEnumerable<Tenant>> GetAllAsync() =>
        await _tenantCollection.Aggregate().Sample(SAMPLE_NUM).ToListAsync();

    public async Task<IEnumerable<Tenant>> GetFilteredByPropertyAsync(string propertyName, string value)
    {
        var property = typeof(Tenant).GetProperty(propertyName,
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

            return await _tenantCollection.Find(Builders<Tenant>.Filter.Eq(property.Name, convertedValue)).ToListAsync();
        }
        catch (Exception)
        {
            throw new PropertyFilterException($"Неверный тип значения '{value}' для поля '{propertyName}'");
        }
    }

    public async Task<Tenant?> GetByIdAsync(string id) =>
        await _tenantCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Tenant newTenant) =>
        await _tenantCollection.InsertOneAsync(newTenant);

    public async Task<bool> UpdateAsync(string id, Tenant updatedTenant)
    {
        updatedTenant.Id = id;
        var result = await _tenantCollection.ReplaceOneAsync(x => x.Id == id, updatedTenant);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteByIdAsync(string id)
    {
        var result = await _tenantCollection.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<bool> DeleteAllAsync()
    {
        var result = await _tenantCollection.DeleteManyAsync(_ => true);

        return result.DeletedCount > 0;
    }
}