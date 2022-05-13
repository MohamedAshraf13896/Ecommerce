
using Project.Models;
using System.Collections.Generic;

namespace Project.ViewModels
{
    public class ProductCategoryVM
    {
        public List<Product> Products { get; set; }
        public List<Category>Categories { get; set; }
        public int NumberOfPages { get; set; }
        public string ActionName { get; set; }

    }
}
