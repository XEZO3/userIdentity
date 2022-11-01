using System.ComponentModel.DataAnnotations.Schema;

namespace userIdentity.Models
{
    public class Order : Main
    {
        [ForeignKey("userAuth")]
        public string UserId { get; set; }
        public string State { get; set; }

       
        public List<OrderItem> orderItems { get; set; }

        public Order()
        {
            orderItems = new List<OrderItem>();
        }
    }
}
