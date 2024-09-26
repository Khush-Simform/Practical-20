using System.ComponentModel.DataAnnotations;

namespace WebApiCore.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName  { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? Token { get; set; }
    }
}
