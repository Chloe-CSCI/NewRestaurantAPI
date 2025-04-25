using NewRestaurantAPI.Models.Entities;

namespace NewRestaurantAPI.Services
{
    public interface IFoodRepository
    {//This holds the CRUDD methods that will be used in the DbRepos and the Controllers,
        // it pulls information from the Food Entity.
        Task<ICollection<Food>> ReadAllAsync(); //ICollection will manipulate generic collection.
        Task<Food> CreateAsync(Food newFood);
        Task<Food?> ReadAsync(int id);
        Task UpdateAsync(int oldId, Food food);
        Task DeleteAsync(int id);
    }
}
