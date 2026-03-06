using MongoDB.Driver;
using Microsoft.Extensions.Options;
using ApartmentRentals.Main.Models;

namespace SpaceStoreApi.Services;

public class LandlordService 
{
    private readonly IMongoCollection<Landlord> _landlordsCollection;

    public LandlordService(IOptions<SpaceStoreDatabaseSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        
        _landlordsCollection = database.GetCollection<Landlord>(
            settings.Value.LandlordsCollectionName);
    }

    public async Task<List<Landlord>> GetAllAsync() =>
        await _landlordsCollection.Find(_ => true).ToListAsync();

    public async Task<Landlord?> GetByIdAsync(string id) =>
        await _landlordsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Landlord landlord)
    {
        //landlord.Id = ObjectId.GenerateNewId().ToString();
        await _landlordsCollection.InsertOneAsync(landlord);
    }

    public async Task<bool> UpdateAsync(string id, Landlord landlord)
    {
        var result = await _landlordsCollection.ReplaceOneAsync(x => x.Id == id, landlord);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> Delete(string id)
    {
        var result = await _landlordsCollection.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }
}