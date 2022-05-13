using Project.Models;
using System.Collections.Generic;

namespace Project.Repositories
{
    public interface IOrderDetail_Repo
    {
        int delete(int id);
        OrderDetails findByid(int id);
        List<OrderDetails> getall();
        int insertMany(List<OrderDetails> order);
        int update(OrderDetails order);
    }
}