using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        public OrdersController(IOrder_Repo order_Repo, IPaymentRepo paymentRepo , IShipperRepo shipperRepo)
        {
            this.orderRepo = order_Repo;
            this.paymentRepo = paymentRepo;
            this.shipperRepo = shipperRepo;
        }

        // GET: Orders
        [Authorize(Roles = "Admin")]
        public  IActionResult Index()
        {
            return View(orderRepo.getall());
        }

        // GET: Orders/Details/5
        [Authorize(Roles = "Customer")]
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

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["PaymentID"] = new SelectList(paymentRepo.GetAll(), "ID", "Type");
            ViewData["ShipperID"] = new SelectList(shipperRepo.GetAll(), "ID", "CompanyName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("ID,OrderNumber,OrderDate,ShipDate,SubTotal,Discount,OrderTax,TotalPrice,IsPaid,ShipperID,PaymentID")] Order order)
        {
            if (ModelState.IsValid)
            {
                orderRepo.insert(order);    
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentID"] = new SelectList(paymentRepo.GetAll(), "ID", "Type");
            ViewData["ShipperID"] = new SelectList(shipperRepo.GetAll(), "ID", "CompanyName");
            return View(order);
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
