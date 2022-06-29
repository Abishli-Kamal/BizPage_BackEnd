using Microsoft.AspNetCore.Mvc;

namespace BizPage_Back_End.Areas.BizPageAdmin.Controllers
{
    [Area("BizPageAdmin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
