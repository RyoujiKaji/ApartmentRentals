namespace ApartmentRentals.Main.Models
{
    public enum SpaceType { Apartment, Room, BedPlace, House, PartOfHouse, Townhouse, Garage }
    public enum HouseType { Brick, Wooden, Monolithic, Panel, Block, MonoBrick, Stalinka }
    public enum RentalTerm { LongTerm, SeveralMonths, ShortTerm }

    public class SpacePhoto
    {
        public string Url { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsMain { get; set; } // Главная картинка
    }

    public class Area
    {
        public double Total { get; set; } // Общая
        public double Kitchen { get; set; } // Кухня
        public double Living { get; set; } // Жилая
    }

    public class Space
    {
        public int Id { get; set; }

        // Основная инфо
        public SpaceType Category { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        // Комнатность и цена
        public int RoomCount { get; set; }
        public decimal Price { get; set; }
        public RentalTerm Term { get; set; }

        // Площадь и этаж
        public Area Area { get; set; } = new();
        public int Floor { get; set; }
        public int TotalFloors { get; set; }

        // Характеристики дома
        public int BuildYear { get; set; }
        public HouseType Material { get; set; }

        // Условия проживания
        public bool AllowsChildren { get; set; }
        public bool AllowsPets { get; set; }

        // фото
        public List<SpacePhoto> Photos { get; set; } = new();

        // Связи
        public int LandlordId { get; set; } = 0;
        public DateTime PublishedAt { get; set; } = DateTime.Now;
    }
}
