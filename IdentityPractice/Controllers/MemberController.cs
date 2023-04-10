using IdentityPractice.Entities;
using IdentityPractice.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace IdentityPractice.Controllers
{
    public class MemberController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public async Task<IActionResult> Index()
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            return View(user);
        }
        public MemberController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(IFormFile photo)
        {
            var user = await _userManager.GetUserAsync(User);

            if (photo != null && photo.Length > 0)
            {
                using (var image = Image.Load(photo.OpenReadStream()))
                {
                    var ratio = (float)500 / image.Width;
                    var height = (int)(image.Height * ratio);
                    image.Mutate(x => x.Resize(500, height));
                    using (var ms = new MemoryStream())
                    {
                        var encoder = new JpegEncoder { Quality = 80 }; // %80 sıkıştırma kalitesi
                        image.Save(ms, encoder);
                        user.ProfilePhoto = ms.ToArray();
                    }
                }
            }

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }
        public IActionResult UploadPhoto()
        {
            AppUser user = new AppUser();
            return View(user);
        }
		public async Task<IActionResult> EditProfile()
        {
			AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            UserCreateModel userCM = new UserCreateModel { Email = user.Email, Gender = user.Gender, ProfilePhoto = user.ProfilePhoto, Username = user.UserName, User=user };

            return View(userCM);
		}
		[HttpPost]
		public async Task<IActionResult> EditProfile(UserCreateModel userCM, IFormFile photo ,string about)
		{
			if (ModelState.IsValid)
			{
				AppUser user = await _userManager.GetUserAsync(HttpContext.User);
                
                user.UserName = userCM.Username;
                user.Email = userCM.Email;
				user.Gender = userCM.Gender;
                user.About = about;

				//IdentityUserRole<int> identityUserRole=  new IdentityUserRole<int> { RoleId=2,UserId=user.Id};

				if (photo != null && photo.Length > 0)
				{
					using (var image = Image.Load(photo.OpenReadStream()))
					{
						var ratio = (float)500 / image.Width;
						var height = (int)(image.Height * ratio);
						image.Mutate(x => x.Resize(500, height));
						using (var ms = new MemoryStream())
						{
							var encoder = new JpegEncoder { Quality = 80 }; // %80 sıkıştırma kalitesi
							image.Save(ms, encoder);
							user.ProfilePhoto = ms.ToArray();
						}
					}
				}

				var identityResult = await _userManager.UpdateAsync(user);

				if (identityResult.Succeeded)
				{
					await _userManager.AddToRoleAsync(user, "MEMBER");
					await _userManager.UpdateAsync(user);


					return RedirectToAction("EditProfile", "Member");
				}

				foreach (var error in identityResult.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}

			return RedirectToAction("EditProfile","Member");
		}

	}
}
