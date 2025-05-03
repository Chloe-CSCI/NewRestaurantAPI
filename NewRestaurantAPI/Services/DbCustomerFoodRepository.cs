using Microsoft.EntityFrameworkCore;
using NewRestaurantAPI.Data;
using NewRestaurantAPI.Models.Entities;

namespace NewRestaurantAPI.Services
{
    public class DbCustomerFoodRepository : ICustomerFoodRepository
    {
        // The injection of the applicationDbContext and the Customer and Food interface repository.
        private readonly ApplicationDbContext _db; 
        private readonly ICustomerRepository _customerRepo;
        private readonly IFoodRepository _foodRepo;

        // This wll help add a method to add entities to the database.
        public DbCustomerFoodRepository(ApplicationDbContext db, ICustomerRepository customerRepo, IFoodRepository foodRepo)// injecting the applicationDbContext cutomer and food interface repos into the repository.
        {
            _db = db;
            _customerRepo = customerRepo;
            _foodRepo = foodRepo;
        }



        public async Task<ICollection<CustomerFood>> ReadAllAsync() //This wil read all the records from the CustomerFood Dataabase, includes query results from customer and food, 
            //and return a new query with the related data.
        {
            return await _db.CustomerFood
                .Include(cf => cf.food)
                .Include(cf => cf.customer)
                .ToListAsync();
        }


        public async Task<CustomerFood?> CreateAsync(int customerId, int foodId)
        {
            var participant = await _customerRepo.ReadAsync(customerId);
            if (participant == null)
            {//this will be if the customer was not found.
                return null;
            }
            var customerPick = participant.CustomersFood
                .FirstOrDefault(cf => cf.FoodId == foodId);
            if(customerPick != null)
            {// This is if the customer already has a food.
                return null;
            }
            var meal = await _foodRepo.ReadAsync(foodId);
            if (meal == null)
            {//this will be if the food was not found.
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


        public async Task<CustomerFood?> ReadAsync(int id) //this will retrieve an object from the database, includes query results from customer and food.
        {// and return a new query with the related data.
            return await _db.CustomerFood
                .Include(cf => cf.food)
                .Include(cf => cf.customer)
                .FirstOrDefaultAsync(cf => cf.Id == id);

        }


        public async Task UpdateCustomerFoodAsync(int customerFoodId, string menuItem) // This will update the customerFood by pulling from the CustomerFood Entity.
        {//this will connect to the post from customerFood Controller. 
            var custFood = await ReadAsync(customerFoodId);
            if (custFood != null)
            {
                custFood.menuItem = menuItem;

                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int customerId, int customerFoodId) // This will delete CustomerFood and saves the changes
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
