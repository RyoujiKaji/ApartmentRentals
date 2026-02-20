namespace ApartmentRentals.Main.DTOs
{
    public class TenantDTO : UserDTO
    {
        public bool HasPets { get; set; }
        public bool HasChildren { get; set; }
    }
}
