using Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Project.Repositories
{
    public class ShipperRepo : IShipperRepo
    {
        Ecomerce db;
        public ShipperRepo(Ecomerce _context)
        {
            db = _context;
        }
        public List<Shipper> GetAll()
        {
            return db.Shippers.ToList();
        }
        public Shipper FindById(int id)
        {
            return db.Shippers.FirstOrDefault(s => s.ID == id);
        }
        public int Edit(int id, Shipper Ship)
        {
            Shipper OldShip = FindById(id);
            if (OldShip == null)
            {
                OldShip.CompanyName = Ship.CompanyName;
                OldShip.PhoneNumber = Ship.PhoneNumber;
                return db.SaveChanges();
            }
            return 0;
        }
        public int Insert(Shipper ship)
        {
            db.Shippers.Add(ship);
            return db.SaveChanges();
        }
        public int Delete(int id)
        {
            Shipper OldShip = FindById(id);
            db.Shippers.Remove(OldShip);
            return db.SaveChanges();
        }
    }
}
