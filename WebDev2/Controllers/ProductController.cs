using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDev2.DataConnection;

namespace WebDev2.Controllers
{
    public class ProductController : Controller
    {
        private readonly MySqlDbContext _context;
        public ProductController(MySqlDbContext context)
        {
            _context = context;
        }
        // GET: ProductController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            Models.Product p = new Models.Product();
            InventoryNamespace.Items items = new InventoryNamespace.Items();
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        Models.Product p = new Models.Product();
        //        p.productname = collection["productname"];
        //        p.description = collection["description"];
        //        p.category = collection["category"];
        //        p.price = Double.Parse(collection["price"]);
        //        //p.datetimeadded = DateTime.Parse(collection["datetimeadded"]);
        //        p.datetimeadded = DateTime.Now;
        //        _context.Products.Add(p);
        //        _context.SaveChanges();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public ActionResult Create([Bind()] Models.Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                        _context.Products.Add(product);
                        _context.SaveChanges();
                        return RedirectToAction(nameof(Index));
                }
                
            }
            catch
            {
                return View();
            }
            return View(product);
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
