using IdentityPractice.Entities;
using IdentityPractice.Models;
using IdentityPractice.Models.Context;
using IdentityPractice.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IdentityPractice.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ContextDeneme _db;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, ContextDeneme db)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
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

		[Authorize(Roles = "Admin")]

		public IActionResult AdminPanel()
        {
            return View();
        }

        [Authorize(Roles = "Member,Admin")]

        public async Task<IActionResult> HomePage(PostVM postVM)
        {

            //AppUser user = await _userManager.GetUserAsync(HttpContext.User);

			postVM.Posts = _db.Posts.Include(x => x.Comments).Include(x => x.Category).ToList();
            postVM.Categories = _db.Categories.ToList();
			_db.Comments.ToList();
            _db.Categories.ToList();

            return View(postVM);

        }


        [HttpPost]
        public IActionResult CategoryList(PostVM postVM,int CategoryId)
        {
            postVM.Posts = _db.Posts.Include(x => x.Category).Where(x => x.CategoryId.Equals(CategoryId)).ToList();
			postVM.Categories = _db.Categories.ToList();



			return View("HomePage",postVM);
        }


        public IActionResult PostPage(int postid)
        {
            //Post post = _db.Posts.Include(x => x.Comments).Where(x => x.Equals(postid)).FirstOrDefault();
            return View(/*post*/);
        }

    }
}