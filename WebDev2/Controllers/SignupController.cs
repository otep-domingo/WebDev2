using Microsoft.AspNetCore.Mvc;

namespace WebDev2.Controllers
{
    public class SignupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
