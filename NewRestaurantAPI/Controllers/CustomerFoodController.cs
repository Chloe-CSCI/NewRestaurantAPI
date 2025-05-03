using NewRestaurantAPI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NewRestaurantAPI.Services;
using Microsoft.AspNetCore.Authorization;


namespace NewRestaurantAPI.Controllers
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



        //be able to create a link with the customer and food.
        
        public async Task<IActionResult> Create(//Takes two parameters.
            [Bind(Prefix ="id")] int customerId, int foodId) // the parameters are the id's of the customer and the food.
        {
            var participant = await _customerRepo.ReadAsync(customerId);
            if(participant == null)
            {
                return RedirectToAction("Index", "Customer");
            }
            var meal = await _foodRepo.ReadAsync(foodId);
            if(meal == null)
            {
                return RedirectToAction("Details", "Customer", new { id = customerId });
            }
            var customerFood = participant.CustomersFood.SingleOrDefault(cf =>cf.FoodId == foodId);
            if (customerFood != null)
            {
                return RedirectToAction("Details", "Customer", new { id = customerId });
            }
            var customerFoodVM = new CustomerFoodVM
            {
                customer = participant,
                food = meal
            };
            return View(customerFoodVM);    
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Create")]
        public async Task<IActionResult> CreateConfirmed(int customerId, int foodId)
        {
            await _customerFoodRepo.CreateAsync(customerId, foodId);
            return RedirectToAction("Details","Customer", new { id = customerId });
        }



        // this will show the customers rating of the food. Taking the parameters of the id of the customer and the food.
     
        public async Task<IActionResult> CustomerMeal(
      [Bind(Prefix = "id")] int customerId, int foodId)
        {
            var participant = await _customerRepo.ReadAsync(customerId);
            if (participant == null)
            {
                return RedirectToAction("Index", "Customer");
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
        public async Task<IActionResult> CustomerMealConfirmed(
            string customerId, int customerFoodId, string menuItem)
        {
            await _customerFoodRepo.UpdateCustomerFoodAsync(
                customerFoodId, menuItem);
            return RedirectToAction("Details", "customer", new { id = customerId });
        }




        // This will be used to delete the customer. While taking the parameter of the id of the customer and the food.
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

        //thiis is used fro Authenticating the usesr and getting the user name.
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
