using Project.Models;
using System.Collections.Generic;

namespace Project.Repositories
{
    public interface IPaymentRepo
    {
        int Delete(int id);
        int Edit(int id, Payment pay);
        Payment FindById(int id);
        List<Payment> GetAll();
        int Insert(Payment pay);
    }
}