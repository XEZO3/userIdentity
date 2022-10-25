using System.ComponentModel.DataAnnotations.Schema;

namespace userIdentity.Models
{
    public class CartItems:Main
    {
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        [ForeignKey("Courses")]
        public int CourseId { get; set; }
        public Courses courses { get; set; }
    }
}
