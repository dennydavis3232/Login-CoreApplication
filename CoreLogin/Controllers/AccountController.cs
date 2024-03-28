using Microsoft.AspNetCore.Mvc;
using CoreLogin.Models;
using System.Linq;
using CoreLogin.Data;

namespace CoreLogin.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid) // Check if model state is valid
            {
                // Query the database to find a user with the provided username and password
                var user = _context.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null) // If user is found in the database
                {
                    // Redirect to dashboard upon successful login
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    // If username or password is incorrect, add an error and return to login page
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    return View(model);
                }
            }
            else
            {
                // If model state is not valid, return to login page with errors
                return View(model);
            }
        }

        // GET: /Account/Index
        public IActionResult Index()
        {
            List<User> items = _context.Users.ToList();
            // Logic to display the dashboard
            return View(items);
        }
        public IActionResult First()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new User object with the provided data
                var newUser = new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    Email = model.Email,
                    PhoneNumber = model.MobileNumber,
                    Position = model.Position
                };

                // Save the new user to the database
                _context.Users.Add(newUser);
                _context.SaveChanges();

                // Redirect to the login page upon successful registration
                return RedirectToAction("Index", "Account");
            }

            // If validation fails, return to the registration page with errors
            return View(model);
        }

    }
}
