using System.ComponentModel.DataAnnotations.Schema;

namespace userIdentity.Models
{
    public class Cart : Main
    {
        [ForeignKey("userAuth")]
        public string UserId { get; set; }
        public userAuth userAuth { get; set; }
        public List<CartItems> cartItems { get; set; }
        public Cart() {
        cartItems = new List<CartItems>();
        }
    }
}
