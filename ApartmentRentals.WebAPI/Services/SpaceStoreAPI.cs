using ApartmentRentals.Main.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace SpaceStoreApi.Services;

public class SpaceService
{
    private readonly IMongoCollection<Space> _spaceCollection;

    public SpaceService(
        IOptions<SpaceStoreDatabaseSettings> spaceStoreDatabaseSettings)
    {
       
        var mongoClient = new MongoClient(
            spaceStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            spaceStoreDatabaseSettings.Value.DatabaseName);

        _spaceCollection = mongoDatabase.GetCollection<Space>(
            spaceStoreDatabaseSettings.Value.SpaceCollectionName);
    }

    public async Task<List<Space>> GetAllAsync() =>
        await _spaceCollection.Find(_ => true).ToListAsync();

    public async Task<Space?> GetByIdAsync(string id) =>
        await _spaceCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Space newSpace) {
        
        await _spaceCollection.InsertOneAsync(newSpace);
    }

    public async Task<bool> UpdateAsync(string id, Space updatedSpace) {
        //await _spaceCollection.ReplaceOneAsync(x => x.Id == /*int.Parse(id)*/ id, updatedSpace);
        var result = await _spaceCollection.ReplaceOneAsync(x => x.Id == id, updatedSpace);
        // Возвращаем true, если документ был изменён (ModifiedCount > 0)
        return result.ModifiedCount > 0;
    }

    public async Task<bool> Delete(string id) {
        //await _spaceCollection.DeleteOneAsync(x => x.Id == /*int.Parse(id)*/ id);
        var result = await _spaceCollection.DeleteOneAsync(x => x.Id == id);
        
        // Возвращаем true, если документ был удалён (DeletedCount > 0)
        return result.DeletedCount > 0;
    }
}