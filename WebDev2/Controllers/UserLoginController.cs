using Microsoft.AspNetCore.Mvc;
using WebDev2.Data;
using WebDev2.Models;
namespace WebDev2.Controllers
{
    public class UserLoginController : Controller
    {
        public const string SessionKeyName = "_Name";
        private readonly MySqlDbContext _context;
        public UserLoginController(MySqlDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Invalid()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Account account)
        {

            if (account != null)
            {
                if (ModelState.IsValid)
                {
                    //check the username fro the database
                    var User = from m in _context.Account select m;
                    User = User.Where(s => s.UserName.Contains(account.UserName));
                    if (User.Count() != 0)
                    {
                        if (User.First().Password == account.Password)
                        {
                            //set the session
                            HttpContext.Session.SetString(SessionKeyName, account.UserName);
                            //redirect to index
                            return View("../Home/Index");
                        }
                    }
                    else
                    {
                        return View("Invalid");
                    }
                }
            }
            return View("Invalid");
        }

        public IActionResult Logout()
        {
            //remove the session
            HttpContext.Session.Clear();
            return View("Index");
        }
    }
}
