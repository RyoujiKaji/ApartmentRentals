namespace ApartmentRentals.Main.DTOs
{
    public class LandlordDTO : UserDTO
    {
        public float Rating { get; set; }
        public List<int> OwnedSpaceIds { get; set; } = new(); // Список ID его объектов
    }
}
