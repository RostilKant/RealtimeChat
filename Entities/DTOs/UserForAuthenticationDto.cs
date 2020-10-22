using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "email is required")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "pass is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }
}