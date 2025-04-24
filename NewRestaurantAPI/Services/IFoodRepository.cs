using NewRestaurantAPI.Models.Entities;

namespace NewRestaurantAPI.Services
{
    public interface IFoodRepository
    {
        Task<ICollection<Food>> ReadAllAsync();
        Task<Food> CreateAsync(Food newFood);
        Task<Food?> ReadAsync(int id);
        Task UpdateAsync(int oldId, Food food);
        Task DeleteAsync(int id);
    }
}
