using Microsoft.EntityFrameworkCore;
using NewRestaurantAPI.Data;
using NewRestaurantAPI.Models.Entities;

namespace NewRestaurantAPI.Services
{
    public class DbTransactionsRepository : ITransactionsRepository
    {

        private readonly ApplicationDbContext _db;
        public DbTransactionsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<Transactions>> ReadAllAsync()
        {
            return await _db.Transactions
                .Include(s => s.CustomersFood)
                .ToListAsync();
        }
        public async Task<Transactions> CreateAsync(Transactions newCustomer)
        {
            await _db.Transactions.AddAsync(newCustomer);
            await _db.SaveChangesAsync();
            return newCustomer;
        }


        public async Task<Transactions?> ReadAsync(int id)
        {
            return await _db.Transactions
                .Include(s => s.CustomersFood)
                .FirstOrDefaultAsync(c => c.Id == id);

        }

        public async Task UpdateAsync(int oldId, Transactions transactions)
        {
            Transactions? transactionsToUpdate = await ReadAsync(oldId);
            if (transactionsToUpdate != null)
            {
                transactionsToUpdate.CustomersMoney = transactions.CustomersMoney;
                transactionsToUpdate.CustomersMoney = transactions.CustomersMoney;
                transactionsToUpdate.CustomersMoney = transactions.CustomersMoney;
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
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
