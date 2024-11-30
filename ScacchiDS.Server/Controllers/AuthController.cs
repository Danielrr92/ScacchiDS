using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScacchiDS.Server.Data;
using ScacchiDS.Server.Models;
using System;

namespace ScacchiDS.Server.Controllers
{

    public class AuthController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest(new { message = "Email e Password sono obbligatori" });

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return Unauthorized(new { message = "Accesso negato" });
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (!isPasswordValid) 
                return Unauthorized(new { message = "Accesso negato" });

            // Generate token or session here
            return Ok(new { message = "Login riuscito", userId = user.Id });
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest(new { message = "Tutti i campi devono essere compilati per la registrazione" });

            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
                return Conflict(new { message = "L'Email inserita è già stata utilizzata." });

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = HashPassword(request.Password), 
                CreatedAt = DateTime.UtcNow,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registrazione avvenuta con successo" });
        }


        public class RegisterRequest
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        private string HashPassword(string password)
        {            
            return BCrypt.Net.BCrypt.HashPassword(password);
        }



    }
}
