using System.Collections.Generic;

namespace Project.Models
{
    public interface IPayment
    {
        int ID { get; set; }
        List<Order> Orders { get; set; }
        int Type { get; set; }
    }
}