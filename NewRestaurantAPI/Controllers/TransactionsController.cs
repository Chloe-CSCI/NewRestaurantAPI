using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewRestaurantAPI.Models.Entities;
using NewRestaurantAPI.Services;
using System.Xml.Serialization;

namespace NewRestaurantAPI.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
       

            private readonly ITransactionsRepository _transactionsRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IFoodRepository _foodRepo;



        public TransactionsController(ITransactionsRepository transactionsRepo, ICustomerRepository customerRepo, IFoodRepository foodRepo)
            {
                _transactionsRepo = transactionsRepo;
            _customerRepo = customerRepo;
            _foodRepo = foodRepo;
        }


            public async Task<IActionResult> Index() // This will show a list of the attributes that is related to the entity of Transactions.
            {
                var restr = await _transactionsRepo.ReadAllAsync();
                return View(restr);
            }


            [HttpPost]
            public async Task<IActionResult> Create(Transactions newTransactions) //this will create new information relateed to what is the transactions repository
            {
                if (ModelState.IsValid)
                {
                    await _transactionsRepo.CreateAsync(newTransactions);
                    return RedirectToAction("Index");
                }
                return View(newTransactions);
            }

        public IActionResult Create()//This will be to create a new customer
        {
            return View();
        }


        



        public async Task<IActionResult> Details(int id) // This will show the details of the transactions.
            {
                var money = await _transactionsRepo.ReadAsync(id);
                if (money == null)
                {
                    return RedirectToAction("Index");
                }
                return View(money);
            }

       


            [HttpPost]
            public async Task<IActionResult> Edit(Transactions transactions) // This will will used to as a way to edit the transactions.
            {
                if (ModelState.IsValid)
                {

                    await _transactionsRepo.UpdateAsync(transactions.Id, transactions);
                    return RedirectToAction("Index");
                }
                return View(transactions);
            }

        public async Task<IActionResult> Edit(int id) // This will edit the transactions.
        {
            var participant = await _transactionsRepo.ReadAsync(id); //having participant refenece to _transactinos will let us get information from the interface repo.
            if (participant == null)
            {
                return RedirectToAction("Index");
            }
            return View(participant);
        }

        public async Task<IActionResult> Delete(int id) // This will be used to delete the transactions.
            {
                var trasactions = await _transactionsRepo.ReadAsync(id);
                if (trasactions == null)
                {
                    return RedirectToAction("Index");
                }
                return View(trasactions);
            }


            [HttpPost, ActionName("Delete")]
            public async Task<IActionResult> DeleteConfirmed(int id)//This will confirm the deletion that was made.
            {
                await _transactionsRepo.DeleteAsync(id);
                return RedirectToAction("Index");

            }


        public IActionResult GetUserName()  //This is for authentication of the user. This will identify them.
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

