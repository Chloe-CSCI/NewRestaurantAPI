using Microsoft.EntityFrameworkCore;
using NewRestaurantAPI.Data;
using NewRestaurantAPI.Models.Entities;

namespace NewRestaurantAPI.Services
{
    public class DbTransactionsRepository : ITransactionsRepository
    {

        private readonly ApplicationDbContext _db;// injecting the applicationDbContext ito the repository.
        public DbTransactionsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<Transactions>> ReadAllAsync() //This wil read all the records from the Transactions Table and return them as a collection.
        {
            return await _db.Transactions
                .Include(t => t.CustomersFood)
                .ToListAsync();
        }
        public async Task<Transactions> CreateAsync(Transactions newCustomer) //This will add an object to the database using the database context, commit the changes, then return the object.
        {
            await _db.Transactions.AddAsync(newCustomer);
            await _db.SaveChangesAsync();
            return newCustomer;
        }


        public async Task<Transactions?> ReadAsync(int id) //this will retrieve an object from the database.
        {
            return await _db.Transactions
                .Include(t => t.CustomersFood)
                .FirstOrDefaultAsync(c => c.Id == id);

        }

        public async Task UpdateAsync(int oldId, Transactions transactions) //this will update the transactions based of what is in its entity and if it is not null and save the changes.
        {
            Transactions? transactionsToUpdate = await ReadAsync(oldId);
            if (transactionsToUpdate != null)
            {
                transactionsToUpdate.CustomersMoney = transactions.CustomersMoney;
                transactionsToUpdate.FoodPrice = transactions.FoodPrice;
                transactionsToUpdate.TotalPrice = transactions.TotalPrice;
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id) // this will delete the Transactions if they are not null then save the changes.
        {
            Transactions? transactionsToDelete = await ReadAsync(id);
            if (transactionsToDelete != null)
            {
                _db.Transactions.Remove(transactionsToDelete);
                await _db.SaveChangesAsync();
            }
        }
    }
}
