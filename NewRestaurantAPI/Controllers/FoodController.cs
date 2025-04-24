using Microsoft.AspNetCore.Mvc;
using NewRestaurantAPI.Models.Entities;
using NewRestaurantAPI.Services;

namespace NewRestaurantAPI.Controllers
{
    public class FoodController : Controller
    {
        public readonly IFoodRepository _foodRepo;


        public FoodController(IFoodRepository foodRepo)
        {
            _foodRepo = foodRepo;
        }


        public async Task<IActionResult> Index()
        {
            var restr = await _foodRepo.ReadAllAsync();
            return View(restr);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Food newfood)
        {
            if (ModelState.IsValid)
            {
                await _foodRepo.CreateAsync(newfood);
                return RedirectToAction("Index");
            }
            return View(newfood);
        }

        public async Task<IActionResult> Details(int id)
        {
            var meal = await _foodRepo.ReadAsync(id);
            if (meal == null)
            {
                return RedirectToAction("Index");
            }
            return View(meal);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(Food food)
        {
            if (ModelState.IsValid)
            {
                await _foodRepo.UpdateAsync(food.Id, food);
                return RedirectToAction("Index");
            }
            return View(food);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var food = await _foodRepo.ReadAsync(id);
            if (food == null)
            {
                return RedirectToAction("Index");
            }
            return View(food);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _foodRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }


    }
}
