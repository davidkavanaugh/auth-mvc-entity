using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AuthMVCEntity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


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
        public IActionResult UserPost(RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View("../Register/RegisterBase");
            }

            PasswordHasher<RegistrationRequest> Hasher = new PasswordHasher<RegistrationRequest>();
            request.Password = Hasher.HashPassword(request, request.Password);

            User user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("UserId", user.UserId);
            return Redirect($"users/{user.UserId}");
        }
        [HttpGet("users/new")]
        public IActionResult RegisterGet()
        {

            return View("../Register/RegisterBase");
        }

        [HttpGet("users/{id}")]
        public IActionResult UserGet()
        {
            return View("../Dashboard/DashboardBase");
        }

        [HttpGet("login")]
        public IActionResult LoginGet()
        {
            return View("../Login/LoginBase");
        }

        [HttpPost("login")]
        public IActionResult LoginPost(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View("../Login/LoginBase");
            }

            User user = _context.Users
                .FirstOrDefault(user => user.Email == request.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "Invalid Email/Password");
                return View("../Login/LoginBase");
            }

            var hasher = new PasswordHasher<LoginRequest>();

            // verify provided password against hash stored in db
            var result = hasher.VerifyHashedPassword(request, user.Password, request.Password);

            if (result == 0)
            {
                ModelState.AddModelError("Email", "Invalid Email/Password");
                return View("../Login/LoginBase");
            }

            return Redirect($"users/{user.UserId}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
