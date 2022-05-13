using Project.Models;
using System.Collections.Generic;

namespace Project.Repositories
{
    public interface IProductRepo
    {
        int DeleteById(int? id);

        int Edit(Product newProduct);
        List<Product> GetAll(int page=0);
        List<Product> GetProductsByCategory(int categoryId, int page);
        Product GetById(int? id);
        Product GetByIdwithCategory(int? id);
        int Insert(Product prod);
        int ProdutsCount();
        int ProdutsCategoryCount(int id);

    }
}