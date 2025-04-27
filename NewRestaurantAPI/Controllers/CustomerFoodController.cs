using NewRestaurantAPI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NewRestaurantAPI.Services;
using Microsoft.AspNetCore.Authorization;


namespace RestaurantAPI.Controllers
{
    [Authorize]
    public class CustomerFoodController : Controller
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IFoodRepository _foodRepo;
        private readonly ICustomerFoodRepository _customerFoodRepo;

        public CustomerFoodController(// These will make it so that this controller can reference to the customer, food, and customerFood interface repos.
        ICustomerRepository customerRepo,
        IFoodRepository foodRepo,
        ICustomerFoodRepository customerFoodRepo)
        {
            _customerRepo = customerRepo;
            _foodRepo = foodRepo;
            _customerFoodRepo = customerFoodRepo;
        }

        public IActionResult Index() // This will show the list associated with customerFood entity.
        {
            return View();
        }




        public async Task<IActionResult> Create( // This will be used to create a new CustoemrId.
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
        [HttpPost, ValidateAntiForgeryToken, ActionName("Create")] // this will confirm the create
        public async Task<IActionResult> CreateConfirmed(int customerId, int foodId)
        {
            await _customerFoodRepo.CreateAsync(customerId, foodId);
            return RedirectToAction("Details", "customer", new { id = customerId });
        }


        // this will show the customers rating of the food.

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


        public IActionResult GetUserName()
        {
            if (User.Identity!.IsAuthenticated)
            {
                string username = User.Identity.Name ?? "";
                return Content(username);
            }
            return Content("No user");
        }
        [Authorize]
        public IActionResult Restricted()
        {
            return Content("This is restricted.");
        }



    }
}
