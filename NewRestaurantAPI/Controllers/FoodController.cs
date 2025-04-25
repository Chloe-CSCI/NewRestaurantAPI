using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewRestaurantAPI.Models.Entities;
using NewRestaurantAPI.Services;

namespace NewRestaurantAPI.Controllers
{
    [Authorize]
    public class FoodController : Controller
    {
        public readonly IFoodRepository _foodRepo;


        public FoodController(IFoodRepository foodRepo)
        {
            _foodRepo = foodRepo;
        }


        public async Task<IActionResult> Index() // This will show a list of the attributes that is related to the entity of Food.
        {
            var restr = await _foodRepo.ReadAllAsync();
            return View(restr);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Food newfood) //This will be to create new food
        {
            if (ModelState.IsValid) //this will see if it is valid.
            {
                await _foodRepo.CreateAsync(newfood);
                return RedirectToAction("Index");
            }
            return View(newfood);
        }

        public async Task<IActionResult> Details(int id) //This will show the details of the food.
        {
            var meal = await _foodRepo.ReadAsync(id);
            if (meal == null)
            {
                return RedirectToAction("Index");
            }
            return View(meal);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(Food food) //This will be used to edit the food.
        {
            if (ModelState.IsValid)
            {
                await _foodRepo.UpdateAsync(food.Id, food);
                return RedirectToAction("Index");
            }
            return View(food);
        }

        public async Task<IActionResult> Delete(int id) //This will be used to delete the Food.
        {
            var food = await _foodRepo.ReadAsync(id);
            if (food == null)
            {
                return RedirectToAction("Index");
            }
            return View(food);
        }


        [HttpPost, ActionName("Delete")] //This will show a confirmation that the delete happened.
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _foodRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }




        public IActionResult GetUserName() //This is for authentication of the user. This will identify them.
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
