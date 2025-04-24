using NewRestaurantAPI.Models.Entities;

namespace NewRestaurantAPI.Services
{
    public interface ICustomerRepository
    {
        Task<ICollection<Customer>> ReadAllAsync();
        Task<Customer> CreateAsync(Customer newCustomer);
        Task<Customer?> ReadAsync(int id);
        Task UpdateAsync(int oldId, Customer customer);
        Task DeleteAsync(int id);
    }
}
