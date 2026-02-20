using ApartmentRentals.Main.Models;
using ApartmentRentals.Main.Repositories;

namespace ApartmentRentals.Data.Repositories
{
    public class LandlordRepository : IRepository<Landlord>
    {
        private readonly List<Landlord> _landlords = [
            new Landlord {
                Id = 1, Name = "Иван", Surname = "Иванов",
                Phone = "+79001112233", Email = "ivan@mail.ru",
                Rating = 4.8f, OwnedSpaceIds = [1]
            },
            new Landlord {
                Id = 2, Name = "Мария", Surname = "Петрова",
                Phone = "+79005556677", Email = "masha@mail.ru",
                Rating = 5.0f, OwnedSpaceIds = [2]
            }
        ];

        private static int _nextId = 3;

        public async Task<IEnumerable<Landlord>> GetAllAsync() => await Task.FromResult(_landlords);

        public async Task<Landlord?> GetByIdAsync(int id) =>
            await Task.FromResult(_landlords.FirstOrDefault(l => l.Id == id));

        public async Task CreateAsync(Landlord entity)
        {
            entity.Id = _nextId++;
            _landlords.Add(entity);
            await Task.CompletedTask;
        }

        public async Task<bool> DeleteById(int id)
        {
            var landlord = _landlords.FirstOrDefault(l => l.Id == id);

            if (landlord == null)
            {
                return false;
            }

            _landlords.Remove(landlord);
            return true;
        }

        public async Task<bool> Update(Landlord entity)
        {
            var index = _landlords.FindIndex(l => l.Id == entity.Id);

            if (index == -1)
            {
                return false;
            }

            _landlords[index] = entity;
            return true;
        }
    }
}