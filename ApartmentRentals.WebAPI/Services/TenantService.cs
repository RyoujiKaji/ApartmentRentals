using MongoDB.Driver;
using Microsoft.Extensions.Options;
using ApartmentRentals.Main.Models;

namespace SpaceStoreApi.Services;

public class TenantService 
{
    private readonly IMongoCollection<Tenant> _tenantCollection;

    public TenantService(
        IOptions<SpaceStoreDatabaseSettings> tenantStoreDatabaseSettings)
    {
       
        var mongoClient = new MongoClient(
            tenantStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            tenantStoreDatabaseSettings.Value.DatabaseName);

        _tenantCollection = mongoDatabase.GetCollection<Tenant>(
            tenantStoreDatabaseSettings.Value.TenantCollectionName);
    }

    public async Task<List<Tenant>> GetAllAsync() =>
        await _tenantCollection.Find(_ => true).ToListAsync();

    public async Task<Tenant?> GetByIdAsync(string id) =>
        await _tenantCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Tenant newTenant) =>
        await _tenantCollection.InsertOneAsync(newTenant);

    public async Task<bool> UpdateAsync(string id, Tenant updatedTenant) {
        //await _spaceCollection.ReplaceOneAsync(x => x.Id == /*int.Parse(id)*/ id, updatedSpace);
        var result = await _tenantCollection.ReplaceOneAsync(x => x.Id == id, updatedTenant);
        // Возвращаем true, если документ был изменён (ModifiedCount > 0)
        return result.ModifiedCount > 0;
    }

    public async Task<bool> Delete(string id) {
        //await _spaceCollection.DeleteOneAsync(x => x.Id == /*int.Parse(id)*/ id);
        var result = await _tenantCollection.DeleteOneAsync(x => x.Id == id);
        
        // Возвращаем true, если документ был удалён (DeletedCount > 0)
        return result.DeletedCount > 0;
    }

    public async Task<bool> DeleteAll()
    {
        var result = await _tenantCollection.DeleteManyAsync(_ => true);

        return result.DeletedCount > 0;
    }
}