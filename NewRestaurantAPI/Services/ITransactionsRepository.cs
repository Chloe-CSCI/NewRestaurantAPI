using NewRestaurantAPI.Models.Entities;

namespace NewRestaurantAPI.Services
{
    public interface ITransactionsRepository
    {
        Task<ICollection<Transactions>> ReadAllAsync();
        Task<Transactions> CreateAsync(Transactions newFood);
        Task<Transactions?> ReadAsync(int id);
        Task UpdateAsync(int oldId, Transactions food);
        Task DeleteAsync(int id);
    }
}
