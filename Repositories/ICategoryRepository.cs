using Project.Models;
using System.Collections.Generic;

namespace Project.Repositories
{
    public interface ICategoryRepository
    {
        int CreateCagtegory(Category Newcategory);
        int DeleteCagtegory(int id);
        List<Category> GetAll();
        List<Category> GetAllWithProducts();
        Category GetById(int? id);
        int UpdateCagtegory(int id, Category Newcategory);
    }
}