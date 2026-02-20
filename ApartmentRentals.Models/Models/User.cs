namespace ApartmentRentals.Main.Models
{
    public abstract class User
    {
        public int Id { get; set; }

        public string Surname { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string? MiddleName { get; set; } = null;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
