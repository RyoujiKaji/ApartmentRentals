using ApartmentRentals.Data.Models;

namespace ApartmentRentals.Data.DTOs
{
    public class SpaceDTO
    {
        public string Id { get; set; }

        public SpaceType Category { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public int RoomCount { get; set; }

        public decimal Price { get; set; }
        public RentalTerm Term { get; set; }

        public Area Area { get; set; } = new();
        public int Floor { get; set; }
        public int TotalFloors { get; set; }

        public int BuildYear { get; set; }
        public HouseType Material { get; set; }

        public bool AllowsChildren { get; set; }
        public bool AllowsPets { get; set; }

        public List<SpacePhoto> Photos { get; set; } = new();

        public string LandlordId { get; set; } = "0";
        public DateTime PublishedAt { get; set; } = DateTime.Now;
    }
}
