using NewRestaurantAPI.Models.Entities;

namespace NewRestaurantAPI.Services
{
    public interface ICustomerFoodRepository
    {//This holds the CRUDD methods that will be used in the DbRepos and the Controllers,
        // it pulls information from the CustomerFood Entity.
        Task<ICollection<CustomerFood>> ReadAllAsync(); //ICollection will manipulate generic collection.
        Task<CustomerFood?> CreateAsync(int customerId, int foodId);
        Task<CustomerFood?> ReadAsync(int id);
        Task UpdateCustomerFoodAsync(int customerFoodId, string menuItem);
        Task DeleteAsync(int customerId, int customerFoodId);
    }
}
