using NewRestaurantAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace NewRestaurantAPI.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer> Customer => Set<Customer>();
        public DbSet<Food> Food => Set<Food>();
        public DbSet<Transactions> Transactions => Set<Transactions>();
        public DbSet<CustomerFood> CustomerFood => Set<CustomerFood>();
    }
}
