using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AuthMVCEntity.Models;

namespace AuthMVCEntity.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;

        private AuthContext _context;

        public AuthController(ILogger<AuthController> logger, AuthContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectToAction("Register");
        }
        [HttpPost("users")]
        public IActionResult CreateUser()
        {
            return RedirectToAction("GetUser");
        }
        [HttpGet("users/new")]
        public IActionResult Register()
        {
            return View("../Register/RegisterBase");
        }

        [HttpGet("users/{id}")]
        public IActionResult GetUser()
        {
            return View("../Dashboard/DashbaordBase");
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View("../Login/LoginBase");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
