using IdentityPractice.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IdentityPractice.Controllers
{
	[Authorize(Roles = "Member,Admin")]

	public class PostController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

        public PostController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult AddPost()
		{


			return View();
		}

		[HttpGet]
		public async Task<IActionResult> AddPost(Post post,int a)
		{
			AppUser user = await _userManager.GetUserAsync(HttpContext.User);
			user.Posts.Add(post);

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddPost(Post post)
		{
			AppUser user = await _userManager.GetUserAsync(HttpContext.User);
			user.Posts.Add(post);

			return RedirectToAction("HomePage","Home");
		}


	}
}
