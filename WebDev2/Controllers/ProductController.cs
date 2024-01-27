using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDev2.Data;
using WebDev2.Models;
namespace WebDev2.Controllers
{
    public class ProductController : Controller
    {
        private readonly MySqlDbContext _context;
        private List<Product> pList = null;
        public ProductController(MySqlDbContext context)
        {
            _context = context;
        }
        // GET: ProductController
        public async Task<IActionResult> Index(string idproducts)
        {
            var products = from p in _context.Products
                           select p;
            if (!String.IsNullOrEmpty(idproducts))
            {
                try
                {
                    int pId = Convert.ToInt32(idproducts);
                    products = products.Where(p => p.idproducts == pId);
                    pList = products.ToList();
                }
                catch (FormatException ee)
                {

                }

            }

            //return View(await _context.Products.ToListAsync());
            return View(await products.ToListAsync());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            Models.Product p = _context.Products.Find(id); 
            
            return View(p);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            List<Category> cat = new List<Category>();
            cat = (from c in _context.Categories
                   select c).ToList();
            cat.Insert(0, new Category { idcategories = 0, category = "--Select--" });
            ViewBag.Categories = cat;
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
            if(id==null || _context.Products == null)
            {
                return NotFound();
            }
            var p = _context.Products.Find(id);

            if (p == null)
            {
                return NotFound();
            }
            return View(p);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind()]Models.Product product)
        {
            if(id != product.idproducts)
            {
                return NotFound();
                
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View(product);
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            Models.Product p = _context.Products.Find(id);

            return View(p);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [Bind()] Models.Product product)
        {


            Models.Product p = _context.Products.Find(id);

           
                try
                {
                    _context.Remove(p);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            
            return View(p);
        }

       
        public ActionResult Print(int id)
        {
            byte[] pdfBytes = null;
            using (var stream = new MemoryStream())
            using (var wri = new PdfWriter(stream))
            using (var pdf = new PdfDocument(wri))
            using (var document = new Document(pdf, iText.Kernel.Geom.PageSize.A4.Rotate(), false))
            {
                //to add a text
                document.Add(new Paragraph("This is a test"));
                //to add a table from the database context
   
                Table table = new Table(2,true);
                Cell h1 = new Cell()
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .Add(new Paragraph("ID"))
                    .SetBold();
                table.AddCell(h1);
                Cell h2 = new Cell()
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .Add(new Paragraph("Product name"))
                    .SetBold();
                table.AddCell(h2);

                //var products = from p in _context.Products
                //               select p;
                //if (!String.IsNullOrEmpty(id))
                //{
                //    try
                //    {
                //        int pId = Convert.ToInt32(id);
                //        products = products.Where(p => p.idproducts == pId);

                //    }
                //    catch (FormatException ee)
                //    {

                //    }

                //}
                foreach (var i in _context.Products.Where(p=>p.idproducts==id))
                {
                    Cell idheader = new Cell()
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .Add(new Paragraph(i.idproducts.ToString()));
                    Cell name = new Cell()
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .Add(new Paragraph(i.productname));
                    table.AddCell(idheader);
                    table.AddCell(name);
                    Border b = new SolidBorder(1.0f);
                    table.SetBorderBottom(b);
                    document.Add(table);
                }
                document.Close();
                document.Flush();
                pdfBytes = stream.ToArray();
            }
            HttpContext.Response.Headers.Add("content-disposition", "inline;filename=" + "sample.pdf");
            return File(fileContents: pdfBytes, "application/pdf");

        }

        public ActionResult PrintAll()
        {
            byte[] pdfBytes = null;
            using (var stream = new MemoryStream())
            using (var wri = new PdfWriter(stream))
            using (var pdf = new PdfDocument(wri))
            using (var document = new Document(pdf, iText.Kernel.Geom.PageSize.A4.Rotate(), false))
            {
                //to add a text
                document.Add(new Paragraph("This is a test"));
                //to add a table from the database context

                Table table = new Table(2, true);
                Cell h1 = new Cell()
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .Add(new Paragraph("ID"))
                    .SetBold();
                table.AddCell(h1);
                Cell h2 = new Cell()
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .Add(new Paragraph("Product name"))
                    .SetBold();
                table.AddCell(h2);

                foreach (var i in _context.Products)
                {
                    Cell idheader = new Cell()
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .Add(new Paragraph(i.idproducts.ToString()));
                    Cell name = new Cell()
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .Add(new Paragraph(i.productname));
                    table.AddCell(idheader);
                    table.AddCell(name);
                    Border b = new SolidBorder(1.0f);
                    table.SetBorderBottom(b);
                    document.Add(table);
                }
                document.Close();
                document.Flush();
                pdfBytes = stream.ToArray();
            }
            HttpContext.Response.Headers.Add("content-disposition", "inline;filename=" + "sample.pdf");
            return File(fileContents: pdfBytes, "application/pdf");

        }
    }
}
