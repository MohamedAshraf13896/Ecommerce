using System.Collections.Generic;

namespace Project.Models
{
    public class Shipper
    {
        public int ID { get; set; }
        public int CompanyName { get; set; }
        public string PhoneNumber { get; set; }

        //Foreign Keys

        //Navigation Properties
        public virtual List<Order>Orders { get; set; }
    }
}
