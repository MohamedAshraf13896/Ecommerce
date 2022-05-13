using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Repositories;

namespace Project.Controllers
{
    public class OrdersController : Controller
    {
        
        private  IOrder_Repo orderRepo;
        private IPaymentRepo paymentRepo;
        private  IShipperRepo shipperRepo;
        private UserManager<ApplicationUser> userManager;

        public OrdersController(IOrder_Repo order_Repo, IPaymentRepo paymentRepo , IShipperRepo shipperRepo,UserManager<ApplicationUser> _userManager)
        {
            this.orderRepo = order_Repo;
            this.paymentRepo = paymentRepo;
            this.shipperRepo = shipperRepo;
            this.userManager = _userManager;
        }

        // GET: Orders
        [Authorize(Roles = "Admin")]
        public  IActionResult Index()
        {
            return View(orderRepo.getall());
        }

        // GET: Orders/Details/5
        //[Authorize(Roles = "Customer")]
        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = orderRepo.findByid(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AdminDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = orderRepo.findByid(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public async Task<IActionResult> Create(string cart)
        {
            Random rnd = new Random();
            string id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            ApplicationUser user = await userManager.FindByIdAsync(id);
            Order currentOrder = new Order()
            {
                OrderNumber = rnd.Next(),
                Address = user.Address,
                OrderDate = DateTime.Now.Date,
                ShipDate = DateTime.Now.AddDays(3).Date,
                Discount = 10,
                OrderTax = 1000,
                IsPaid = false
            };
            int result = orderRepo.insert(currentOrder);
            if(result == -1)
            {
                return Content("Operation Failed");
            }
            OrderDetails orderDetails = new OrderDetails();
            return View();
        }

        // GET: Orders/Edit/5
        public  IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = orderRepo.findByid(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["PaymentID"] = new SelectList(paymentRepo.GetAll(), "ID", "Type");
            ViewData["ShipperID"] = new SelectList(shipperRepo.GetAll(), "ID", "CompanyName");
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, [Bind("ID,OrderNumber,OrderDate,ShipDate,SubTotal,Discount,OrderTax,TotalPrice,IsPaid,ShipperID,PaymentID")] Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    orderRepo.update(order);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentID"] = new SelectList(paymentRepo.GetAll(), "ID", "Type");
            ViewData["ShipperID"] = new SelectList(shipperRepo.GetAll(), "ID", "CompanyName");
            return View(order);
        }

        // GET: Orders/Delete/5
        public  IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = orderRepo.findByid(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = orderRepo.delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return orderRepo.getall().Any(e => e.ID == id);
        }
    }
}
