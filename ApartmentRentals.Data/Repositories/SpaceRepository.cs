using ApartmentRentals.Main.Repositories;
using ApartmentRentals.Main.Models;

namespace ApartmentRentals.Data.Repositories
{
    public class SpaceRepository : IRepository<Space>
    {
        private readonly List<Space> _spaces =
        [
            new Space
            {
                Id = 1,
                Title = "Уютная двухкомнатная квартира в центре",
                Category = SpaceType.Apartment,
                Address = "Москва, ул. Тверская, д. 12, кв. 45",
                RoomCount = 2,
                Price = 85000,
                Term = RentalTerm.LongTerm,
                Area = new Area { Total = 60, Kitchen = 12, Living = 35 },
                Floor = 5,
                TotalFloors = 12,
                BuildYear = 1980,
                Material = HouseType.Brick,
                AllowsChildren = true,
                AllowsPets = false,
                LandlordId = 1,
                Photos = new List<SpacePhoto>
                {
                    new SpacePhoto { Url = "https://example.com/photo1.jpg", Description = "Гостиная", IsMain = true },
                    new SpacePhoto { Url = "https://example.com/photo2.jpg", Description = "Кухня", IsMain = false }
                },
                PublishedAt = DateTime.Now.AddDays(-2)
            },
            new Space
            {
                Id = 2,
                Title = "Светлая комната для студента",
                Category = SpaceType.Room,
                Address = "Санкт-Петербург, пр. Просвещения, д. 5",
                RoomCount = 1,
                Price = 15000,
                Term = RentalTerm.LongTerm,
                Area = new Area { Total = 18, Kitchen = 9, Living = 18 },
                Floor = 2,
                TotalFloors = 9,
                BuildYear = 1975,
                Material = HouseType.Panel,
                AllowsChildren = false,
                AllowsPets = true,
                LandlordId = 2,
                Photos = new List<SpacePhoto>
                {
                    new SpacePhoto { Url = "https://example.com/photo3.jpg", Description = "Вид из окна", IsMain = true }
                },
                PublishedAt = DateTime.Now.AddHours(-5)
            }
        ];

        // Используем string для ID, чтобы потом было легче переехать на MongoDB (там ObjectId — это строка)
        private static int _nextId = 3;

        public async Task<IEnumerable<Space>> GetAllAsync()
        {
            return await Task.FromResult(_spaces);
        }

        public async Task<Space?> GetByIdAsync(int id)
        {
            var space = _spaces.FirstOrDefault(s => s.Id == id);
            return await Task.FromResult(space);
        }

        public async Task CreateAsync(Space entity)
        {
            entity.Id = _nextId++;
            _spaces.Add(entity);
            await Task.CompletedTask;
        }

        public async Task<bool> Update(Space entity)
        {
            var index = _spaces.FindIndex(s => s.Id == entity.Id);

            if (index == -1)
            {
                return false;
            }

            _spaces[index] = entity;
            return true;
        }

        public async Task<bool> DeleteById(int id)
        {
            var space = _spaces.FirstOrDefault(l => l.Id == id);

            if (space == null)
            {
                return false;
            }

            _spaces.Remove(space);
            return true;
        }
    }
}