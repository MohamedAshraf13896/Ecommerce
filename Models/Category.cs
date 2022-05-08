using System.Collections.Generic;

namespace Project.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public bool IsActive { get; set; }

        //Foreign Keys
        //Navigation Properties
        public virtual List<Product> Products { get; set; }
    }
}
