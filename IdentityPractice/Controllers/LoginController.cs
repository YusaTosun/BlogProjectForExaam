using IdentityPractice.Entities;
using IdentityPractice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Diagnostics;
using System.Security.Claims;

namespace IdentityPractice.Controllers
{
    public class LoginController : Controller
    {

		private readonly RoleManager<AppRole> _roleManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager; //todo:bunların kaç tanesi gerekli bak @@@@@@@@@@@@@@
		private readonly ILogger<HomeController> _logger;

		public LoginController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
		{
			_logger = logger;
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}
	
		public IActionResult SignUp()
		{


			return View(new UserCreateModel());
		}


		[HttpPost]
		public async Task<IActionResult> SignUp(UserCreateModel model,IFormFile photo = null)
		{

			if (ModelState.IsValid)
			{
				AppUser user = new AppUser()
				{
					UserName = model.Username,
					Email = model.Email,
					Gender = model.Gender

				};

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

                var identityResult = await _userManager.CreateAsync(user, model.Password);

				if (identityResult.Succeeded)
				{
					await _userManager.AddToRoleAsync(user, "MEMBER");
					await _userManager.UpdateAsync(user);


					return RedirectToAction("SignIn", "Login");
				}

				foreach (var error in identityResult.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}

			return View(model);
		}

		public IActionResult SignIn(string returnUrl)
		{
			
			return View(new UserSignInModel { ReturnUrl = returnUrl });
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(UserSignInModel model)
		{
			if (ModelState.IsValid)
			{
				var signInResult = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, true);


				if (signInResult.Succeeded)
				{
					if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
					{
						return Redirect(model.ReturnUrl);
					}

					var user = await _userManager.FindByNameAsync(model.Username);
					var roles = await _userManager.GetRolesAsync(user);

					if (roles.Contains("Admin"))
					{
						return RedirectToAction("AdminPanel");
					}
					//else if (roles.Contains("Member"))
					//{
					//	return RedirectToAction("Panel");
					//}

					//Başarılı Giriş
				}

				else if (signInResult.IsLockedOut)
				{
					//hesap kilitli
				}

				else if (signInResult.IsNotAllowed)
				{
					//e-mail&& Phone number doğrulanmış

				}
			}

			return View(model);

		}
	}
}
