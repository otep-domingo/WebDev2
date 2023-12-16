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

        public IActionResult Calculator()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public ActionResult CalculateBill(IFormCollection form)
        {
            var bill = Convert.ToInt32(form["currentbill"].ToString()) - Convert.ToInt32(form["previousbill"].ToString());
            float rate = 0.0f;
            float charge = 0.0f;
            if (bill < 200 && bill > 0)
            {
                charge = 1.2f;
            }else if(bill>=200 && bill <= 400)
            {
                charge = 1.50f;
            }else if(bill>=401 && bill <= 600)
            {
                charge = 1.80f;
            }
            else
            {
                charge = 2.0f;
            }
            var newRate = charge * 12.50;
            var total = bill * newRate;
            // return "Your total bill is " + total;
            ViewBag.total = total;
            ViewBag.newrate = newRate;
            ViewBag.previous = form["previousbill"].ToString();
            ViewBag.current = form["currentbill"].ToString();
            ViewBag.bill = bill;
            return View("Calculator");
        }

        [HttpPost]
        public string SubmitForm(Account user)
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
                    return $"User Created: UserName: {user.UserName}";
                }
            }
            return "Some Error Occured";
        }
    }
}