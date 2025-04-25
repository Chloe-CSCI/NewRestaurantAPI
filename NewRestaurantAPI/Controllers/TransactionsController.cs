using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewRestaurantAPI.Models.Entities;
using NewRestaurantAPI.Services;

namespace NewRestaurantAPI.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
       

            private readonly ITransactionsRepository _transactionsRepo;



            public TransactionsController(ITransactionsRepository transactionsRepo)
            {
                _transactionsRepo = transactionsRepo;
            }


            public async Task<IActionResult> Index()
            {
                var restr = await _transactionsRepo.ReadAllAsync();
                return View(restr);
            }


            [HttpPost]
            public async Task<IActionResult> Create(Transactions newTransactions)
            {
                if (ModelState.IsValid)
                {
                    await _transactionsRepo.CreateAsync(newTransactions);
                    return RedirectToAction("Index");
                }
                return View(newTransactions);
            }

            public async Task<IActionResult> Details(int id)
            {
                var money = await _transactionsRepo.ReadAsync(id);
                if (money == null)
                {
                    return RedirectToAction("Index");
                }
                return View(money);
            }



            [HttpPost]
            public async Task<IActionResult> Edit(Transactions transactions)
            {
                if (ModelState.IsValid)
                {

                    await _transactionsRepo.UpdateAsync(transactions.Id, transactions);
                    return RedirectToAction("Index");
                }
                return View(transactions);
            }

            public async Task<IActionResult> Delete(int id)
            {
                var trasactions = await _transactionsRepo.ReadAsync(id);
                if (trasactions == null)
                {
                    return RedirectToAction("Index");
                }
                return View(trasactions);
            }


            [HttpPost, ActionName("Delete")]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                await _transactionsRepo.DeleteAsync(id);
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

