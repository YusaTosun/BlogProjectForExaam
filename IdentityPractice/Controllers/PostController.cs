using IdentityPractice.Entities;
using IdentityPractice.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IdentityPractice.Controllers
{
	[Authorize(Roles = "Member,Admin")]

	public class PostController : Controller
	{
		private readonly ContextDeneme _db;
		private readonly UserManager<AppUser> _userManager;

		public PostController(UserManager<AppUser> userManager, ContextDeneme db)
		{
			_userManager = userManager;
			_db = db;
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
			post.AuthorId = user.Id;
			post.CategoryId = 2;
			user.Posts.Add(post);
			_db.SaveChanges();
			return RedirectToAction("HomePage","Home");
		}


	}
}
