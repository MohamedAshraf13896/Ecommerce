using Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace Project.Repositories
{

    public class PaymentRepo : IPaymentRepo
    {
        Ecomerce db;
        public PaymentRepo(Ecomerce _context)
        {
            db = _context;
        }
        public List<Payment> GetAll()
        {
            return db.Payments.ToList();
        }
        public Payment FindById(int id)
        {
            return db.Payments.FirstOrDefault(s => s.ID == id);
        }
        public int Edit(int id, Payment pay)
        {
            Payment oldPay = FindById(id);
            if (oldPay == null)
            {
                oldPay.Type = pay.Type;
                return db.SaveChanges();
            }
            return 0;
        }
        public int Insert(Payment pay)
        {
            db.Payments.Add(pay);
            return db.SaveChanges();
        }
        public int Delete(int id)
        {
            Payment OldPay = FindById(id);
            db.Payments.Remove(OldPay);
            return db.SaveChanges();
        }
    }
}
