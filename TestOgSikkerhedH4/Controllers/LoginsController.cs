using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestOgSikkerhedH4.Controllers.Models;
using TestOgSikkerhedH4.Controllers.Models.Login;

namespace TestOgSikkerhedH4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly Dbcontext _context;

        public LoginsController(Dbcontext context)
        {
            _context = context;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] Login request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.CPR))
            {
                return BadRequest(new { Message = "Email, password, and CPR are required" });
            }

            // ✅ Check if user already exists
            var existingUser = await _context.login.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                return Conflict(new { Message = "User with this email already exists" });
            }

            // ✅ Hash Password & CPR before saving
            request.Password = HashHelper.HashBCrypt(request.Password);
            request.CPR = HashHelper.HashCPR(request.CPR);

            // ✅ Save user to database
            await _context.login.AddAsync(request);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "User registered successfully!",
                UserID = request.UserID,
                Email = request.Email
            });
        }


        [HttpGet("Login")]
        public async Task<IActionResult> Login([FromQuery] string email, [FromQuery] string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest(new { Message = "Email and password are required" });
            }

            var user = await _context.login.FirstOrDefaultAsync(s => s.Email == email);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            // ✅ Verify Password
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return Unauthorized(new { Message = "Incorrect password" });
            }

            return Ok(new
            {
                UserID = user.UserID,
                Name = user.Name,
                Email = user.Email,
                Status = user.Status,
                CPR = user.CPR //
            });
        }

    }
}

