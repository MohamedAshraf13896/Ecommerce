using Project.Models;
using System.Collections.Generic;

namespace Project.Repositories
{
    public interface IShipperRepo
    {
        int Delete(int id);
        int Edit(int id, Shipper Ship);
        Shipper FindById(int id);
        List<Shipper> GetAll();
        int Insert(Shipper ship);
    }
}