using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShoritifierMVC.Configuration;
using ShoritifierMVC.Intrerfaces;
using ShoritifierMVC.Models;
using ShoritifierMVC.Services;
using System.Security.Claims;

namespace ShoritifierMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        private async Task SignInUserWithCookie(User user)
        {
            var claims = new List<Claim>{
                new Claim("Email", user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("Password", user.Password)
            };

            if (user.Email == "admin@admin.com")
            {
                claims.Add(new Claim("Admin", "true"));
            }

            var identity = new ClaimsIdentity(claims, Config.CookieName);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(Config.CookieName, claimsPrincipal);
        }

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(Config.CookieName);
            return Redirect("/Home/index");
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var existingUser = await userService.LoginUser(user);

            if (existingUser == null)
            {
                ViewData["Error"] = "Your email or password are incorrect";
                return View();
            }
            await SignInUserWithCookie(existingUser);

            return Redirect("/home/index");

        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(User user)
        {
            if (!ModelState.IsValid)
                return View();
            await Task.Run(() => { userService.AddUser(user); });
            return Redirect("/Account/Login");
        }
    }
}
