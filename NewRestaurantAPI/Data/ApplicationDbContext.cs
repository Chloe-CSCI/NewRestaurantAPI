using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewRestaurantAPI.Models.Entities;
using NewRestaurantAPI.Services;

namespace NewRestaurantAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // these entities are being queried and an instance of them is being saved by the use of DbSet
        // The Set returns a set for the given entity type.
        public DbSet<Customer> Customer => Set<Customer>();
        public DbSet<Food> Food => Set<Food>();
        public DbSet<Transactions> Transactions => Set<Transactions>();
        public DbSet<CustomerFood> CustomerFood => Set<CustomerFood>();
    }
}
