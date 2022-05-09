using Project.Models;
using System.Collections.Generic;

namespace Project.Repositories
{
    public interface IOrder_Repo
    {
        int delete(int id);
        Order findByid(int id);
        List<Order> getall();
        int insert(Order order);
        int update(Order order);
    }
}