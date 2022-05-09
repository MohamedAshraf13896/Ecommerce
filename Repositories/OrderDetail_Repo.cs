using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Project.Models;
namespace Project.Repositories
{
    public class OrderDetail_Repo : IOrderDetail_Repo
    {
        Ecomerce dp; 
        public OrderDetail_Repo(Ecomerce _dp)
        {
            dp = _dp;
        }
        public List<OrderDetails> getall()
        {
            return dp.OrderDetails.ToList();
        }
        public OrderDetails findByid(int id)
        {
            return dp.OrderDetails.FirstOrDefault(n => n.OrderID == id);

        }
        public int insert(OrderDetails order)
        {
            dp.OrderDetails.Add(order);
            return dp.SaveChanges();
        }
        public int update(OrderDetails order)
        {
            OrderDetails old = findByid(order.OrderID);
            if (old != null)
            {
                old.LinePrice = order.LinePrice;
                old.ProductID = order.ProductID;
                old.Quantity = order.Quantity;
                old.Discount = order.Discount;


                return dp.SaveChanges();
            }
            return 0;
        }
        public int delete(int id)
        {
            OrderDetails old = findByid(id);
            dp.OrderDetails.Remove(old);
            return dp.SaveChanges();
        }

    }

}
