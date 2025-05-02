using Microsoft.EntityFrameworkCore;
using NewRestaurantAPI.Data;
using NewRestaurantAPI.Models.Entities;

namespace NewRestaurantAPI.Services
{
    public class DbCustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _db;
        public DbCustomerRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        // we want to read a customer.
        public async Task<Customer?> ReadAsync(int id)
        {
            return await _db.Customer
                .Include(c => c.CustomersFood)
                    .ThenInclude(cf => cf.food)
                .FirstOrDefaultAsync(c => c.Id == id);

        }

        public async Task<ICollection<Customer>> ReadAllAsync()
        {
            return await _db.Customer
                .Include(c => c.CustomersFood)
                    .ThenInclude(cf => cf.food)
                
                .ToListAsync();
        }

        public async Task<Customer> CreateAsync(Customer newCustomer)
        {
            await _db.Customer.AddAsync(newCustomer);
            await _db.SaveChangesAsync();
            return newCustomer;
        }

        public async Task UpdateAsync(int oldId, Customer customer)
        {
            Customer? customerToUpdate = await ReadAsync(oldId);
            if (customerToUpdate != null)
            {
                customerToUpdate.Name = customer.Name;
                customerToUpdate.PhoneNumber = customer.PhoneNumber;
                customerToUpdate.ReservationNumber = customer.ReservationNumber;
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            Customer? customerToDelete = await ReadAsync(id);
            if (customerToDelete != null)
            {
                _db.Customer.Remove(customerToDelete);
                await _db.SaveChangesAsync();
            }
        }

       
    }
}
