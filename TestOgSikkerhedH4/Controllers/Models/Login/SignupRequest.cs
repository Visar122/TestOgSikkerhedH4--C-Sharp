using System.ComponentModel.DataAnnotations;

namespace TestOgSikkerhedH4.Controllers.Models.Login
{
    public class SignupRequest
    {
        [Required]
        public string Name { get; set; } = "";

        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
    }
}
