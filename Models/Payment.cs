using System.Collections.Generic;

namespace Project.Models
{
    public class Payment
    {
        public int ID { get; set; }
        public int Type { get; set; }

        //Foreign Keys

        //Navigation Properties
        public virtual List<Order>Orders { get; set; }

    }
}
