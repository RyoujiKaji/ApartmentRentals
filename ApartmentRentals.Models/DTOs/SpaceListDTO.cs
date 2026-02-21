using ApartmentRentals.Main.Models;

namespace ApartmentRentals.Main.DTOs
{
    public class SpaceListDTO
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public SpaceType Category { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; } = string.Empty;
        public string MainPhotoUrl { get; set; } = string.Empty; // Остается только главное фото для списка
    }
}
