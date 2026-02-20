using ApartmentRentals.Main.Models;
using ApartmentRentals.Main.Repositories;

namespace ApartmentRentals.Data.Repositories
{
    public class RentalContractRepository : IRepository<RentalContract>
    {
        private readonly List<RentalContract> _contracts = [
            new RentalContract {
                Id = 1, SpaceId = 1, TenantId = 1,
                StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(6)
            }
        ];

        private static int _nextId = 2;

        public async Task<IEnumerable<RentalContract>> GetAllAsync() => await Task.FromResult(_contracts);

        public async Task<RentalContract?> GetByIdAsync(int id) =>
            await Task.FromResult(_contracts.FirstOrDefault(c => c.Id == id));

        public async Task CreateAsync(RentalContract entity)
        {
            entity.Id = _nextId++;
            _contracts.Add(entity);
            await Task.CompletedTask;
        }

        public async Task<bool> Update(RentalContract entity)
        {
            var index = _contracts.FindIndex(s => s.Id == entity.Id);

            if (index == -1)
            {
                return false;
            }

            _contracts[index] = entity;
            return true;
        }

        public async Task<bool> DeleteById(int id)
        {
            var contract = _contracts.FirstOrDefault(l => l.Id == id);

            if (contract == null)
            {
                return false;
            }

            _contracts.Remove(contract);
            return true;
        }
    }
}