using System.ComponentModel.DataAnnotations;

namespace Investment_back.Models.Request
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string password { get; set; }
    }
}

