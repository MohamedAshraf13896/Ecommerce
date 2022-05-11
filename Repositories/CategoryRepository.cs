using Project.Models;
using System.Linq;
using System.Collections.Generic;

namespace Project.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        Ecomerce context;
        public CategoryRepository(Ecomerce context)
        {
            this.context = context;
        }

        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }
        public Category GetById(int? id)
        {
            return context.Categories.SingleOrDefault(c => c.ID == id);
        }
        public int CreateCagtegory(Category Newcategory)
        {
            try
            {
                context.Categories.Add(Newcategory);
                return context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public int UpdateCagtegory(int id, Category Newcategory)
        {
            try
            {
                Category oldCat = context.Categories.SingleOrDefault(c => c.ID == id);
                if (oldCat != null)
                {
                    oldCat.Img = Newcategory.Img;
                    oldCat.Name = Newcategory.Name;
                    oldCat.Description = Newcategory.Description;
                    oldCat.IsActive = Newcategory.IsActive;
                }
                return context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public int DeleteCagtegory(int id)
        {
            int rowEffected = 0;
            try
            {
                Category oldCat = context.Categories.SingleOrDefault(c => c.ID == id);
                if (oldCat != null)
                {
                    context.Categories.Remove(oldCat);
                    rowEffected = context.SaveChanges();
                }
                return rowEffected;
            }
            catch (System.Exception)
            {
                throw;
            }
        }


    }
}
