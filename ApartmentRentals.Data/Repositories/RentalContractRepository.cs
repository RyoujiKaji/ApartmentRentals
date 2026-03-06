using ApartmentRentals.Main.Models;
using ApartmentRentals.Main.Repositories;

namespace ApartmentRentals.Data.Repositories
{
    public class RentalContractRepository : IRepository<RentalContract>
    {
        private readonly List<RentalContract> _contracts = [
            new RentalContract {
                Id = "1", SpaceId = "1", TenantId = "1",
                StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(6)
            }
        ];

        private static int _nextId = 2;

        public Task<IEnumerable<RentalContract>> GetAllAsync() => Task.FromResult<IEnumerable<RentalContract>>(_contracts);

        public Task<RentalContract?> GetByIdAsync(string id) =>
            Task.FromResult(_contracts.FirstOrDefault(c => c.Id == id));

        public Task CreateAsync(RentalContract entity)
        {

            entity.Id = Convert.ToString(_nextId++);
            _contracts.Add(entity);

            return Task.CompletedTask;
        }

        public Task<bool> UpdateAsync(RentalContract entity)
        {
            var index = _contracts.FindIndex(s => s.Id == entity.Id);

            if (index == -1)
            {
                return Task.FromResult(false);
            }

            _contracts[index] = entity;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteByIdAsync(string id)
        {
            var contract = _contracts.FirstOrDefault(l => l.Id == id);

            if (contract == null)
            {
                return Task.FromResult(false);
            }

            _contracts.Remove(contract);
            return Task.FromResult(true);
        }
    }
}