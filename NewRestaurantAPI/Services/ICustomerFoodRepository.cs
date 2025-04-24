using NewRestaurantAPI.Models.Entities;

namespace NewRestaurantAPI.Services
{
    public interface ICustomerFoodRepository
    {
        Task<ICollection<CustomerFood>> ReadAllAsync();
        Task<CustomerFood?> CreateAsync(int customerId, int foodId);
        Task<CustomerFood?> ReadAsync(int id);
        Task UpdateCustomerFoodAsync(int customerFoodId, string menuItem);
        Task DeleteAsync(int customerId, int customerFoodId);
    }
}
