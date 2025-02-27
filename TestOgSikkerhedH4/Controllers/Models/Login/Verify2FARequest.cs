using System.ComponentModel.DataAnnotations;

namespace TestOgSikkerhedH4.Controllers.Models.Login
{
    public class Verify2FARequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string TwoFactorCode { get; set; } = "";
    }
}
