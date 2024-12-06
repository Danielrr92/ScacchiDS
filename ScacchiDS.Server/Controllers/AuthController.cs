using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScacchiDS.Server.Data;
using ScacchiDS.Server.Models;
using System;

namespace ScacchiDS.Server.Controllers
{

    public class AuthController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<AuthController> logger) : Controller()
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ILogger<AuthController> _logger = logger;

        public IActionResult LoginAdministrator()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    if (_ApplicationUser != null)
            //    {
            //        //Check data password scaduta
            //        DateTime? dataScaduta = CheckPasswordScaduta(_ApplicationUser);

            //        //Controllo privacy 
            //        string criptData = CheckPrivacyAccettata(_ApplicationUser);
            //        if (criptData != null && criptData != "")
            //        {
            //            return RedirectToActionPermanent("Privacy", new { enc = criptData });
            //        }

            //        if (dataScaduta != null)
            //        {
            //            return RedirectToAction("PasswordScaduta", new { userId = _ApplicationUser.Id, dataScadenzaPassword = dataScaduta });
            //        }
            //        else
            //        {
            //            return RedirectToAction("Index", "Home", new { area = "" });
            //        }
            //    }
            //}


            return View();
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequest request)
        //{
        //    _logger.LogInformation("Chiamata api al controller AuthController riuscita. Action Login " + request.ToString());
        //    if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        //        return BadRequest(new { message = "Email e Password sono obbligatori" });

        //    //var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
        //    //if (user == null)
        //    //    return Unauthorized(new { message = "Accesso negato" });
        //    //bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
        //    //if (!isPasswordValid) 
        //    //    return Unauthorized(new { message = "Accesso negato" });

        //    //// Generate token or session here
        //    return Ok(new { message = "Login riuscito", userId = 1 });
        //}



        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        //{
        //    if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        //        return BadRequest(new { message = "Tutti i campi devono essere compilati per la registrazione" });

        //    //var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
        //    //if (existingUser != null)
        //    //    return Conflict(new { message = "L'Email inserita è già stata utilizzata." });

        //    //var user = new User
        //    //{
        //    //    Username = request.Username,
        //    //    Email = request.Email,
        //    //    Password = HashPassword(request.Password), 
        //    //    CreatedAt = DateTime.UtcNow,
        //    //};

        //    //_context.Users.Add(user);
        //    //await _context.SaveChangesAsync();

        //    return Ok(new { message = "Registrazione avvenuta con successo" });
        //}


        //public class RegisterRequest
        //{
        //    public string Username { get; set; }
        //    public string Email { get; set; }
        //    public string Password { get; set; }
        //}

        //private string HashPassword(string password)
        //{            
        //    return BCrypt.Net.BCrypt.HashPassword(password);
        //}



    }
}
