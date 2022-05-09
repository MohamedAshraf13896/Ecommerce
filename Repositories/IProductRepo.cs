using Project.Models;
using System.Collections.Generic;

namespace Project.Repositories
{
    public interface IProductRepo
    {
        int DeleteById(int id);
        int Edit(Product newProduct);
        List<Product> GetAll();
        Product GetById(int id);
        int Insert(Product prod);
    }
}