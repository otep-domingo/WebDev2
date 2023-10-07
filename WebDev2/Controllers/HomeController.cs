using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebDev2.Models;

namespace WebDev2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public string SubmitForm(User user)
        {
            //var UserName = form["UserName"].ToString();
            //var UserEmail = form["UserEmails"].ToString();
            //// Handle data as needed...
            //return $"User Created: UserName: {UserName}, UserEmail: {UserEmail}";

            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    // Handle data as needed...
                    return $"User Created: UserName: {user.UserName}, UserEmail: {user.UserEmail}";
                }
            }
            return "Some Error Occured";
        }
    }
}