using ApartmentRentals.Main.Models;
using ApartmentRentals.Main.Repositories;

namespace ApartmentRentals.Data.Repositories
{
    public class TenantRepository //: IRepository<Tenant>
    {
        private readonly List<Tenant> _tenants = [
            new Tenant {
                Id = "1", Name = "Алексей", Surname = "Сидоров", MiddleName = "Олегович",
                HasPets = true, HasChildren = false
            }
        ];

        private static int _nextId = 2;

        public Task<IEnumerable<Tenant>> GetAllAsync() => Task.FromResult<IEnumerable<Tenant>>(_tenants);

        public Task<Tenant?> GetByIdAsync(string id) => Task.FromResult(_tenants.FirstOrDefault(t => t.Id == id));

        public Task CreateAsync(Tenant entity)
        {
            entity.Id = Convert.ToString(_nextId++);
            _tenants.Add(entity);
            return Task.CompletedTask;
        }

        public Task<bool> UpdateAsync(Tenant entity)
        {
            var index = _tenants.FindIndex(s => s.Id == entity.Id);

            if (index == -1)
            {
                return Task.FromResult(false);
            }

            _tenants[index] = entity;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteByIdAsync(string id)
        {
            var tenant = _tenants.FirstOrDefault(l => l.Id == id);

            if (tenant == null)
            {
                return Task.FromResult(false);
            }   

            _tenants.Remove(tenant);
            return Task.FromResult(true);
        }
    }
}