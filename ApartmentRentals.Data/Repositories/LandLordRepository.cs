using ApartmentRentals.Main.Models;
using ApartmentRentals.Main.Repositories;

namespace ApartmentRentals.Data.Repositories
{
    public class LandlordRepository : IRepository<Landlord>
    {
        private readonly List<Landlord> _landlords = [
           /* new Landlord {
                Id = 1, Name = "Иван", Surname = "Иванов", MiddleName = "Иванович",
                Phone = "+79001112233", Email = "ivan@mail.ru",
                Rating = 4.8f, OwnedSpaceIds = [1]
            },
            new Landlord {
                Id = 2, Name = "Мария", Surname = "Петрова", MiddleName= "Сергеевна",
                Phone = "+79005556677", Email = "masha@mail.ru",
                Rating = 5.0f, OwnedSpaceIds = [2]
            }*/
        ];

        private static int _nextId = 0;

        public Task<IEnumerable<Landlord>> GetAllAsync() => Task.FromResult<IEnumerable<Landlord>>(_landlords);

        public Task<Landlord?> GetByIdAsync(string id) =>
            Task.FromResult(_landlords.FirstOrDefault(l => l.Id == id));

        public Task CreateAsync(Landlord entity)
        {
            entity.Id = Convert.ToString(_nextId++);
            _landlords.Add(entity);

            return Task.CompletedTask;
        }

        public Task<bool> DeleteByIdAsync(string id)
        {
            var landlord = _landlords.FirstOrDefault(l => l.Id == id);

            if (landlord == null)
            {
                return Task.FromResult(false);
            }

            _landlords.Remove(landlord);
            return Task.FromResult(true);
        }

        public Task<bool> UpdateAsync(Landlord entity)
        {
            var index = _landlords.FindIndex(l => l.Id == entity.Id);

            if (index == -1)
            {
                return Task.FromResult(false);
            }

            _landlords[index] = entity;
            return Task.FromResult(true);
        }
    }
}