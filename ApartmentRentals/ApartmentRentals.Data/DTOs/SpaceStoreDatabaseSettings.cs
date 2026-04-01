using ApartmentRentals.Models;

public class SpaceStoreDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string SpaceCollectionName { get; set; } = null!;
    public string LandlordsCollectionName { get; set; } = null!;
    public string RentalContractCollectionName { get; set; } = null!;
    public string TenantCollectionName { get; set; } = null!;

}