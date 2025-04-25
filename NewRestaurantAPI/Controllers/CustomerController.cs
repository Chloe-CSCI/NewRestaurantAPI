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

        public async Task<IActionResult> Index() // This will show a list of the attributes that is related to the entity of Customer.
        {
            var restr = await _customerRepo.ReadAllAsync();
            return View(restr);
        }




        [HttpPost]
        public async Task<IActionResult> Create(Customer newCustomer)//This will be to create a new customer
        {
            if (ModelState.IsValid)
            {
                await _customerRepo.CreateAsync(newCustomer);
                return RedirectToAction("Index");
            }
            return View(newCustomer);
        }



        public async Task<IActionResult> Details(int id) // This will show the details of Customer
        {
            var participant = await _customerRepo.ReadAsync(id);
            if (participant == null)
            {
                return RedirectToAction("Index");
            }
            return View(participant);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)//This will let the user edit/update Customer Information.
        {
            if (ModelState.IsValid) // This will check if it is valid, and will update if it is.
            {
                await _customerRepo.UpdateAsync(customer.Id, customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }



        public async Task<IActionResult> Delete(int id) //This will be used to delete customers.
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
