using Microsoft.AspNetCore.Mvc;

namespace IdentityPractice.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult LoginPage()
        {
            return View();
        }
    }
}
