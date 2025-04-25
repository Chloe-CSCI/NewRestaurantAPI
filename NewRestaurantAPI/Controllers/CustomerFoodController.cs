using NewRestaurantAPI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NewRestaurantAPI.Services;


namespace RestaurantAPI.Controllers
{
    public class CustomerFoodController : Controller
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IFoodRepository _foodRepo;
        private readonly ICustomerFoodRepository _customerFoodRepo;

        public CustomerFoodController(
        ICustomerRepository customerRepo,
        IFoodRepository foodRepo,
        ICustomerFoodRepository customerFoodRepo)
        {
            _customerRepo = customerRepo;
            _foodRepo = foodRepo;
            _customerFoodRepo = customerFoodRepo;
        }

        public IActionResult Index()
        {
            return View();
        }




        public async Task<IActionResult> Create(
        [Bind(Prefix = "id")] int customerId, int foodId)
        {
            var participant = await _customerRepo.ReadAsync(customerId);
            if (participant == null)
            {
                return RedirectToAction("Index", "customer");
            }
            var meal = await _foodRepo.ReadAsync(foodId);
            if (meal == null)
            {
                return RedirectToAction("Details", "customer", new { id = customerId });
            }
            var customerFood = meal.CustomerFoods
                .SingleOrDefault(cf => cf.FoodId == foodId);
            if (customerFood != null)
            {
                return RedirectToAction("Details", "customer", new { id = customerId });
            }
            var CustomerFoodVM = new CustomerFoodVM
            {
                customer = participant,
                food = meal
            };
            return View(CustomerFoodVM);
        }
        [HttpPost, ValidateAntiForgeryToken, ActionName("Create")]
        public async Task<IActionResult> CreateConfirmed(int customerId, int foodId)
        {
            await _customerFoodRepo.CreateAsync(customerId, foodId);
            return RedirectToAction("Details", "customer", new { id = customerId });
        }


        // this will show the customer and their choice of meal together

        public async Task<IActionResult> CusomertMeal(
      [Bind(Prefix = "id")] int customerId, int foodId)
        {
            var participant = await _customerRepo.ReadAsync(customerId);
            if (participant == null)
            {
                return RedirectToAction("Index", "customer");
            }
            var customerFood = participant.CustomersFood
                .FirstOrDefault(cf => cf.FoodId == foodId);
            if (customerFood == null)
            {
                return RedirectToAction("Details", "customer", new { id = customerId });
            }
            return View(customerFood);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("CusomertMeal")]
        public async Task<IActionResult> CusomertMealConfirmed(
            string customerId, int customerFoodId, string menuItem)
        {
            await _customerFoodRepo.UpdateCustomerFoodAsync(
                customerFoodId, menuItem);
            return RedirectToAction("Details", "customer", new { id = customerId });
        }

        // This will be used to delete the customer.
        public async Task<IActionResult> Delete(
      [Bind(Prefix = "id")] int customerId, int foodId)
        {
            var participant = await _customerRepo.ReadAsync(customerId);
            if (participant == null)
            {
                return RedirectToAction("Index", "customer");
            }
            var customerFood = participant.CustomersFood
                     .FirstOrDefault(cf => cf.FoodId == foodId);
            if (customerFood == null)
            {
                return RedirectToAction("Details", "customer", new { id = customerId });
            }
            return View(customerFood);
        }
        // this will show that the customer was successfully deleted.
        [HttpPost, ValidateAntiForgeryToken, ActionName("Remove")]
        public async Task<IActionResult> RemoveConfirmed(
            int customerId, int customerFoodId)
        {
            await _customerFoodRepo.DeleteAsync(customerId, customerFoodId);
            return RedirectToAction("Details", "customer", new { id = customerId });
        }






    }
}
