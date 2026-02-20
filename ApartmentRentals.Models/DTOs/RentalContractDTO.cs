namespace ApartmentRentals.Main.DTOs
{
    public class RentalContractDTO
    {
        public int SpaceId { get; set; } = 0;
        public int TenantId { get; set; } = 0;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
