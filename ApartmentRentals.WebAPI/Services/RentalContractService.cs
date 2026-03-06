using ApartmentRentals.Main.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace SpaceStoreApi.Services;

public class RentalContractService
{
    private readonly IMongoCollection<RentalContract> _rentalContractCollection;

    public RentalContractService(
        IOptions<SpaceStoreDatabaseSettings> rentalContractStoreDatabaseSettings)
    {
       
        var mongoClient = new MongoClient(
            rentalContractStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            rentalContractStoreDatabaseSettings.Value.DatabaseName);

        _rentalContractCollection = mongoDatabase.GetCollection<RentalContract>(
            rentalContractStoreDatabaseSettings.Value.RentalContractCollectionName);
    }

    public async Task<List<RentalContract>> GetAllAsync() =>
        await _rentalContractCollection.Find(_ => true).ToListAsync();

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

    public async Task<bool> Delete(string id) {
        //await _spaceCollection.DeleteOneAsync(x => x.Id == /*int.Parse(id)*/ id);
        var result = await _rentalContractCollection.DeleteOneAsync(x => x.Id == id);
        
        // Возвращаем true, если документ был удалён (DeletedCount > 0)
        return result.DeletedCount > 0;
    }
}