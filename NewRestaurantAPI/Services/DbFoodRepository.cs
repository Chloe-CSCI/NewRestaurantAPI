using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NewRestaurantAPI.Data;
using NewRestaurantAPI.Models.Entities;

namespace NewRestaurantAPI.Services
{
    public class DbFoodRepository : IFoodRepository //this will let the DbFoodRepository pull from the IFoodRepository
    {
        private readonly ApplicationDbContext _db; // this will let the database sets from the ApplicationDbContext be pulled and used in this repo.
        public DbFoodRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Food> CreateAsync(Food newFood)// this will be able to create a new food, get from database, and save the changes.
        {
            await _db.AddRangeAsync(newFood);
            await _db.SaveChangesAsync();
            return newFood;
        }

        public async Task DeleteAsync(int id)// delete the food by the id, check if it is or is not there, remove it, and save the changes.
        {
            Food? foodToDelete = await ReadAsync(id);
            if (foodToDelete != null)
            {
                _db.Food.Remove(foodToDelete);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Food>> ReadAllAsync()// read all that is in the Food.
        {
            return await _db.Food
                .Include(f => f.CustomerFoods)
                .ThenInclude(cf => cf.customer)
                .ToListAsync();
        }

        public async Task<Food?> ReadAsync(int id)//read a food.
        {
            return await _db.Food
                .Include(f => f.CustomerFoods)
                .ThenInclude(cf => cf.customer)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(int oldId, Food food)//updates the food, checks if there is something to update, updates accordingly, then saves changes.
        {
            Food? foodToUpdate = await ReadAsync(oldId);
            if (foodToUpdate != null)
            {
                foodToUpdate.FoodName = food.FoodName;
                foodToUpdate.Description = food.Description;
                foodToUpdate.Ingredients = food.Ingredients;
                await _db.SaveChangesAsync();
            }
        }
    }
    
}
