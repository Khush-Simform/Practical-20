using System.ComponentModel.DataAnnotations;

namespace WebApiCore.Models
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string UserName { get; set; } 
        [Required]
        public string MobilePhone { get; set; } 
        [Required]
        public string Email { get; set; }   
        [Required]
        public string Password { get; set; } 
    }
}
