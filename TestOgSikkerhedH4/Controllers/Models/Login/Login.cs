using System.ComponentModel.DataAnnotations;

namespace TestOgSikkerhedH4.Controllers.Models.Login
{
    public class Login
    {
        [Key]
        public int UserID { get; set; }

        public string Name { get; set; } = "";

        public string Email { get; set; } = "";

        public string Password { get; set; } = "";

        public string Status { get; set; } = ""; // ✅ Default status before 2FA is verified

        public string CPR { get; set; } = ""; // ✅ Hashed CPR Number
    }
}
