using Microsoft.EntityFrameworkCore;
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

        public List<Product> GetAll(int page = 0)
        {
            return db.Products.Include(p => p.Category).Skip(page * 9).Take(9).ToList();
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

        public Product GetByIdwithCategory(int? id)
        {
            return db.Products.Include(p => p.Category).SingleOrDefault(p => p.ID == id);
        }

        public List<Product> GetProductsByCategory(int categoryId, int page)
        {
            return db.Products.Where(p => p.CategoryID == categoryId).Skip(page * 9).Take(9).ToList();
        }

        public List<Product> getAllAdmin()
        {
            return db.Products.Include(p => p.Category).ToList();
        }
        public int ProdutsCount()
        {
            return db.Products.Count();
        }

        public int ProdutsCategoryCount(int id)
        {
            return db.Products.Where(p => p.CategoryID == id).Count();
        }

        public List<Product> GetProductsByName(string name, int page)
        {
            var res = db.Products.Where(p => p.Name.Contains(name)).Skip(page * 9).Take(9).ToList();
            return res;
        }

        public int ProdutsNameCount(string ProductName)
        {
            return db.Products.Where(p => p.Name.Contains(ProductName)).Count();
        }

        public List<Product> GetProductsByNameAdmin(string name)
        {
            var res = db.Products.Include(p=>p.Category).Where(p => p.Name.Contains(name)).ToList();
            return res;

        }

      
    }
    }
