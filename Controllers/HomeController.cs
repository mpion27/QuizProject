using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizProject.Models;
using QuizProject.Services;
using QuizProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuizProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IDataProtector _dataProtector;

        public HomeController(ILogger<HomeController> logger, IDataProtectionProvider dataProvider)
        {
            _logger = logger;
            _userRepository = new UserRepository();
            _dataProtector = dataProvider.CreateProtector("secret");
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            ViewBag.Message = TempData["message"];
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {


            if (!ModelState.IsValid)
            {
                model.Message = "";
                model.Password = "";

                return View(model);
            }


            var user = _userRepository.GetUser(model.Username, model.Password);
            if (user != null)
            {
                
                var claims = new List<Claim>
                {
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("FullName", user.UserName),
                    new Claim(ClaimTypes.Role, "Administrator"),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Profile");
            }
            else
            {
                model.Message = "Login or password is invalid";
                return View(model);
            }

        }

        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
