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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Create()
        {


            return View(new UserCreateModel());
        }


        [HttpPost]
        public async Task<IActionResult> Create(UserCreateModel model)
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

                

                var identityResult = await _userManager.CreateAsync(user, model.Password);

                if (identityResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "MEMBER");
                    await _userManager.UpdateAsync(user);
                   

                    return View("Index");
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
            //ViewBag.ReturnUrl = returnUrl;
            return View(new UserSignInModel { ReturnUrl=returnUrl});
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
                    else if(roles.Contains("Member"))
                    {
                        return RedirectToAction("Panel");
                    }

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

        [Authorize()]
        public IActionResult GetUserInfo()
        {
            var userName = User.Identity.Name;
            var role = User.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.Role); // buraya detaylıca bak

            User.IsInRole("Member");
            return View();
        }


        [Authorize(Roles ="Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }

        [Authorize(Roles = "Member")]
        public IActionResult Panel()
        {
            return View();   //// todo:bu actiona girmiyor.View'ı bulamıyor neden ?????
        }

        [Authorize(Roles ="Member")]
        public IActionResult MemberPage() 
        {
        
            return View();
        }

    }
}