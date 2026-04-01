namespace ApartmentRentals.Data.DTOs
{
    public abstract class UserDTO
    {
        public string Id { get; set; }

        public string Surname { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string? MiddleName { get; set; } = null;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
