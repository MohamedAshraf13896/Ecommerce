using Project.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private Ecomerce db;
        public ProductRepo(Ecomerce _db)
        {
            db = _db;
        }

        public List<Product> GetAll()
        {
            return db.Products.ToList();
        }

        public Product GetById(int? id)
        {
            return db.Products.SingleOrDefault(p => p.ID == id);
        }

        public int Insert(Product prod)
        {
            if (prod != null)
            {
                db.Products.Add(prod);
                int rowsAffected = db.SaveChanges();
                return rowsAffected;
            }
            return 0;
        }

        public int Edit(Product newProduct)
        {
            Product oldProduct = GetById(newProduct.ID);
            if (oldProduct != null)
            {
                oldProduct.Name = newProduct.Name;
                oldProduct.Description = newProduct.Description;
                oldProduct.Color = newProduct.Color;
                oldProduct.UnitPrice = newProduct.UnitPrice;
                oldProduct.Discount = newProduct.Discount;
                oldProduct.Img = newProduct.Img;
                oldProduct.Rate = newProduct.Rate;
                oldProduct.CategoryID = newProduct.CategoryID;
                int rowsAffected = db.SaveChanges();
                return rowsAffected;
            }
            return 0;
        }

        public int DeleteById(int? id)
        {
            Product productToDelete = GetById(id);
            db.Products.Remove(productToDelete);
            return db.SaveChanges();
        }
    }
}
