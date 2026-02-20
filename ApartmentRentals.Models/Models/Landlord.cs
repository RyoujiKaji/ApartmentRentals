namespace ApartmentRentals.Main.Models
{
    public class Landlord:User
    {
        public float Rating { get; set; }
        public List<int> OwnedSpaceIds { get; set; } = new(); // Список ID его объектов
    }
}
