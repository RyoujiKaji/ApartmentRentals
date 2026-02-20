namespace ApartmentRentals.Main.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<bool> DeleteById(int id);
        Task CreateAsync(T entity);
        Task<bool> Update(T entity);
    }
}
