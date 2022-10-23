using System.ComponentModel.DataAnnotations;

namespace userIdentity.Models.VM
{
    public class RegisterVM
    {
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
