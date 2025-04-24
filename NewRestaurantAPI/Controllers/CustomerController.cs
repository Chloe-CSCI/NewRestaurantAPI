using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using NewRestaurantAPI.Models.Entities;
using NewRestaurantAPI.Services;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace RestaurantAPI.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepo;



        public CustomerController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<IActionResult> Index()
        {
            var restr = await _customerRepo.ReadAllAsync();
            return View(restr);
        }




        [HttpPost]
        public async Task<IActionResult> Create(Customer newCustomer)
        {
            if (ModelState.IsValid)
            {
                await _customerRepo.CreateAsync(newCustomer);
                return RedirectToAction("Index");
            }
            return View(newCustomer);
        }



        public async Task<IActionResult> Details(int id)
        {
            var participant = await _customerRepo.ReadAsync(id);
            if (participant == null)
            {
                return RedirectToAction("Index");
            }
            return View(participant);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerRepo.UpdateAsync(customer.Id, customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerRepo.ReadAsync(id);
            if (customer == null)
            {
                return RedirectToAction("Index");
            }
            return View(customer);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _customerRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
