using Microsoft.AspNetCore.Mvc;

namespace BizPage_Back_End.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        
    }
}
