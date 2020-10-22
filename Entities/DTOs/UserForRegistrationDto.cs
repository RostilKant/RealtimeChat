using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class UserForRegistrationDto
    {
        [Required(ErrorMessage = "email is required")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "username is required")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "pass is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        /*[Required(ErrorMessage = "please, confirm password!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords mismatch!")]
        public string ConfirmPassword { get; set; }*/
        
        public ICollection<string> Roles { get; set; }
    }
}