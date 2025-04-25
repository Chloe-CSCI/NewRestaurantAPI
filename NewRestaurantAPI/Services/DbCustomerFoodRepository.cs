using Microsoft.EntityFrameworkCore;
using NewRestaurantAPI.Data;
using NewRestaurantAPI.Models.Entities;

namespace NewRestaurantAPI.Services
{
    public class DbCustomerFoodRepository : ICustomerFoodRepository
    {

        private readonly ApplicationDbContext _db;
        private readonly ICustomerRepository _customerRepo;
        private readonly IFoodRepository _foodRepo;

        public DbCustomerFoodRepository(ApplicationDbContext db, ICustomerRepository customerRepo, IFoodRepository foodRepo)// injecting the applicationDbContext cutomer and food interface repos into the repository.
        {
            _db = db;
            _customerRepo = customerRepo;
            _foodRepo = foodRepo;
        }



        public async Task<ICollection<CustomerFood>> ReadAllAsync()
        {
            return await _db.CustomerFood
                .Include(cf => cf.food)
                .Include(cf => cf.customer)
                .ToListAsync();
        }


        public async Task<CustomerFood> CreateAsync(int customerId, int foodId)
        {
            var participant = await _customerRepo.ReadAsync(customerId);
            if (participant == null)
            {
                return null;
            }
            var customersFood = participant.CustomersFood
                .FirstOrDefault(cf => cf.FoodId == foodId);
            if (customersFood != null)
            {
                return null;
            }
            var meal = await _foodRepo.ReadAsync(foodId);
            if (meal == null)
            {
                return null;
            }

            var customerFood = new CustomerFood
            {
                customer = participant,
                food = meal
            };
            participant.CustomersFood.Add(customerFood);
            meal.CustomerFoods.Add(customerFood);
            await _db.SaveChangesAsync();
            return customerFood;

        }


        public async Task<CustomerFood?> ReadAsync(int id)
        {
            return await _db.CustomerFood
                .Include(cf => cf.food)
                .Include(cf => cf.customer)
                .FirstOrDefaultAsync(cf => cf.Id == id);

        }


        public async Task UpdateCustomerFoodAsync(int customerFoodId, string menuItem)
        {
            var custFood = await ReadAsync(customerFoodId);
            if (custFood != null)
            {
                custFood.menuItem = menuItem;

                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int customerId, int customerFoodId)
        {

            var participant = await _customerRepo.ReadAsync(customerId);
            var custFood = participant!.CustomersFood
                .FirstOrDefault(cf => cf.FoodId == customerFoodId);
            var meal = custFood!.food;
            participant!.CustomersFood.Remove(custFood);
            meal!.CustomerFoods.Remove(custFood);
            await _db.SaveChangesAsync();



        }

    }
}
