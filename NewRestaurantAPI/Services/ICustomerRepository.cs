using NewRestaurantAPI.Models.Entities;

namespace NewRestaurantAPI.Services
{
    public interface ICustomerRepository
    {//This holds the CRUDD methods that will be used in the DbRepos and the Controllers,
        // it pulls information from the Customer Entity.
        Task<ICollection<Customer>> ReadAllAsync(); //ICollection will manipulate generic collection.
        Task<Customer> CreateAsync(Customer newCustomer);
        Task<Customer?> ReadAsync(int id);
        Task UpdateAsync(int oldId, Customer customer);
        Task DeleteAsync(int id);
    }
}
