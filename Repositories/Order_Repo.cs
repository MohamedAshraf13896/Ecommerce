using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Project.Models;
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
            return dp.Orders.ToList();
        }
        public Order findByid(int id)
        {
            return dp.Orders.FirstOrDefault(n => n.ID == id);

        }
        public int insert(Order order)
        {
            dp.Orders.Add(order);
            return dp.SaveChanges();
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

                return dp.SaveChanges();
            }
            return 0;
        }
        public int delete(int id)
        {
            Order old = findByid(id);
            dp.Orders.Remove(old);
            return dp.SaveChanges();
        }
    }
}
