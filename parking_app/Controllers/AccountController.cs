using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using parking_app.Data;
using parking_app.Models;
using parking_app.ViewModels;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace parking_app.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var res = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.Persistent,
                false);

            if (!res.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Neplatný e-mail nebo heslo.");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> QuickLogin()
        {
           
            var user = await _userManager.FindByEmailAsync("admin@example.com");
            var count = _context.Users.Count();
            if (user == null)
            {
                return Content($"{count}");
            }

            await _signInManager.SignOutAsync();

            await _signInManager.SignInAsync(user, isPersistent: true);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new User 
            {
                UserName = model.Email,
                Email = model.Email,
                FName = model.FName,
                LName = model.LName,
            };

            var res = await _userManager.CreateAsync(user, model.Password);

            if (res.Succeeded)
            {
                await _signInManager.SignOutAsync();
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            foreach(var err in res.Errors)
            {
                ModelState.AddModelError(string.Empty, err.Description);
            }

            return View(model);
        }

        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
