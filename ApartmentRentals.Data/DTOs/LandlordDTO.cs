namespace ApartmentRentals.Data.DTOs
{
    public class LandlordDTO:UserDTO
    {
        public float Rating { get; set; }
        public List<string> OwnedSpaceIds { get; set; } = new();
    }
}
