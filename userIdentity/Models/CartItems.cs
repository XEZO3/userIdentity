using System.ComponentModel.DataAnnotations.Schema;

namespace userIdentity.Models
{
    public class CartItems:Main
    {
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        [ForeignKey("Courses")]
        public int CoursesId { get; set; } 
        public Courses courses { get; set; }
        public Cart cart { get; set; }
    }
}
