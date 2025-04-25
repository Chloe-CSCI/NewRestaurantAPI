using NewRestaurantAPI.Models.Entities;

namespace NewRestaurantAPI.Services
{
    public interface ITransactionsRepository
    {//This holds the CRUDD methods that will be used in the DbRepos and the Controllers,
        // it pulls information from the Transaction Entity.
        Task<ICollection<Transactions>> ReadAllAsync();//ICollection will manipulate generic collection.
        Task<Transactions> CreateAsync(Transactions newFood);
        Task<Transactions?> ReadAsync(int id);
        Task UpdateAsync(int oldId, Transactions food);
        Task DeleteAsync(int id);
    }
}
