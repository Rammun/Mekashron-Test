using System.ComponentModel.DataAnnotations;

namespace MekashronWeb.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
