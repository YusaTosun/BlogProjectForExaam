using IdentityPractice.Entities;
using IdentityPractice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace IdentityPractice.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager,SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        //[Authorize()]  
        //public IActionResult GetUserInfo()
        //{
        //    var userName = User.Identity.Name;
        //    var role = User.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.Role); // buraya detaylıca bak

        //    User.IsInRole("Member");
        //    return View();
        //}

        [Authorize(Roles ="Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }
      
        [Authorize(Roles = "Member")]

        public IActionResult HomePage()
        {
            return View();

		}

	}
}