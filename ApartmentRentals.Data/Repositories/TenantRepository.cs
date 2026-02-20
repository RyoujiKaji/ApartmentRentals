using ApartmentRentals.Main.Models;
using ApartmentRentals.Main.Repositories;
using System.Diagnostics.Contracts;

namespace ApartmentRentals.Data.Repositories
{
    public class TenantRepository : IRepository<Tenant>
    {
        private readonly List<Tenant> _tenants = [
            new Tenant {
                Id = 1, Name = "Алексей", Surname = "Сидоров",
                HasPets = true, HasChildren = false
            }
        ];

        private static int _nextId = 2;

        public async Task<IEnumerable<Tenant>> GetAllAsync() => await Task.FromResult(_tenants);

        public async Task<Tenant?> GetByIdAsync(int id) =>
            await Task.FromResult(_tenants.FirstOrDefault(t => t.Id == id));

        public async Task CreateAsync(Tenant entity)
        {
            entity.Id = _nextId++;
            _tenants.Add(entity);
            await Task.CompletedTask;
        }

        public async Task<bool> Update(Tenant entity)
        {
            var index = _tenants.FindIndex(s => s.Id == entity.Id);

            if (index == -1)
            {
                return false;
            }

            _tenants[index] = entity;
            return true;
        }

        public async Task<bool> DeleteById(int id)
        {
            var tenant = _tenants.FirstOrDefault(l => l.Id == id);

            if (tenant == null)
            {
                return false;
            }

            _tenants.Remove(tenant);
            return true;
        }
    }
}