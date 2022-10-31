using System.ComponentModel.DataAnnotations.Schema;

namespace userIdentity.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order order { get; set; }
        [ForeignKey("Courses")]
        public int CoursesId { get; set; }
        public Courses courses { get; set; }
    }
}
