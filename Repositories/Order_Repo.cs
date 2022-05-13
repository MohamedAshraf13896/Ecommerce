using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Project.Models;
using System;

namespace Project.Repositories
{
    public class Order_Repo : IOrder_Repo
    {
        Ecomerce dp;
        public Order_Repo(Ecomerce _dp)
        {
            dp = _dp;
        }
        public List<Order> getall()
        {
            return dp.Orders.Include(o => o.Payment).Include(o => o.Shipper).ToList();
        }
        public Order findByid(int? id)
        {
            return dp.Orders.Include(o => o.Payment).Include(o => o.Shipper).Include(o=>o.Customer).Include(o=>o.OrderDetails).ThenInclude(o=>o.Product).FirstOrDefault(n => n.ID == id);
        }
        public int insert(Order order)
        {
            try
            {
                dp.Orders.Add(order);
                dp.SaveChanges();
                int id = dp.Orders.SingleOrDefault(o => o.OrderNumber == order.OrderNumber).ID;
                return id;
            }
            catch(Exception e)
            {
                 return -1;
            }
        }
        public int update(Order order)
        {
            Order old = findByid(order.ID);
            if (old != null)
            {
                old.OrderNumber = order.OrderNumber;
                old.OrderTax = order.OrderTax;
                old.OrderDate = order.OrderDate;
                old.ShipDate = order.ShipDate;
                old.PaymentID = order.PaymentID;
                old.ShipperID = order.ShipperID;
                old.TotalPrice = order.TotalPrice;
                old.IsPaid = order.IsPaid;
                old.SubTotal = order.SubTotal;
                old.Address = order.Address;
                return dp.SaveChanges();
            }
            return 0;
        }
        public int delete(int? id)
        {
            Order old = findByid(id);
            dp.Orders.Remove(old);
            return dp.SaveChanges();
        }
    }
}
