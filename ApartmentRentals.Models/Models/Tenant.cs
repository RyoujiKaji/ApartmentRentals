namespace ApartmentRentals.Main.Models
{
    public class Tenant:User
    {
        public bool HasPets { get; set; }
        public bool HasChildren { get; set; }
    }
}
