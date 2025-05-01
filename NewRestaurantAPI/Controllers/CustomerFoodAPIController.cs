using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewRestaurantAPI.Services;
using NewRestaurantAPI.Models.ViewModels;
namespace NewRestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerFoodAPIController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IFoodRepository _foodRepo;
        private readonly ICustomerFoodRepository _customerFoodRepo;// This wll get the CRUDD Async information frome the interface repo and
        // the _customerFoodRepo will help give us something to reference to.

        public CustomerFoodAPIController(// This will help takg the information for the customer, food, and customerFood Interface repos.
        ICustomerRepository customerRepo,
        IFoodRepository foodRepo,
        ICustomerFoodRepository customerFoodRepo)
        {
            _customerRepo = customerRepo;
            _foodRepo = foodRepo;
            _customerFoodRepo = customerFoodRepo;
        }


        [HttpPost("create")]//JSON
        public async Task<IActionResult> PostAsync([FromForm] int customerId, [FromForm] int foodId)
        { // This will be used to create and clear the customer and food id's from the associated entities.
            var customerFood = await _customerFoodRepo.CreateAsync(customerId, foodId);
            customerFood?.customer?.CustomersFood.Clear();
            customerFood?.food?.CustomerFoods.Clear();
            return CreatedAtAction("Get", new { id = customerFood?.Id }, customerFood);
        }


        [HttpPut("CusomertMeal")] // this is used to update the custoemrFood repo.
        public async Task<IActionResult> PutAsync(
            [FromForm] int customerId,
            [FromForm] int customerFoodId,
            [FromForm] string menuItem)
        {
            await _customerFoodRepo.UpdateCustomerFoodAsync(customerFoodId, menuItem);
            return NoContent();
        }

        // this is the api call to delete the customerId, and the customerFoodId.
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(
            [FromForm] int customerId,
            [FromForm] int customerFoodId)
        {
            await _customerFoodRepo.DeleteAsync(customerId, customerFoodId);
            return NoContent();
        }




        public IActionResult GetUserName()//This is for authentication of the user. This will identify them.
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
