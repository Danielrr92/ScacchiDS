using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScacchiDS.Server.Data;
using ScacchiDS.Server.Models;
using System;

namespace ScacchiDS.Server.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Iniezione del contesto nel costruttore
        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
