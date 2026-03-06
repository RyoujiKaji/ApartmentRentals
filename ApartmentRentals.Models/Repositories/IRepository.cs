namespace ApartmentRentals.Main.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task<bool> DeleteByIdAsync(string id);
        Task CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
    }
}
