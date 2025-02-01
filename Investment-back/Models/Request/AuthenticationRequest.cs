using System.ComponentModel.DataAnnotations;
namespace Investment_back.Models.Request
{
    public class AuthenticationRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        public string username {get; set;}
        [Required(ErrorMessage = "Password is required.")]
        public string password {get; set;}
    }
}
