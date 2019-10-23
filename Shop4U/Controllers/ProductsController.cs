using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Shop4U.Models;

namespace Shop4U.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Shop4UContext _context;
        private readonly IFileProvider fileProvider;
        private readonly IHostingEnvironment hostingEnvironment;



        public ProductsController(Shop4UContext context, IFileProvider fileprovider, IHostingEnvironment env)
        {
            _context = context;
            fileProvider = fileprovider;
            hostingEnvironment = env;
        }


        //GET Index With Search
        public async Task<IActionResult> Index(string product_category, string DescriptionString, string ColorString, string TakenString)
        {

            try
            {
                if (IsAdminOrSupplier())
                {
                    var products = from m in _context.Products
                                   select m;

                    if (!String.IsNullOrEmpty(product_category))
                    {
                        products = products.Where(s => s.product_category == GetCategory(product_category));
                    }
                    if (!String.IsNullOrEmpty(DescriptionString))
                    {

                        products = products.Where(s => s.product_description.Contains(DescriptionString));
                    }
                    if (!String.IsNullOrEmpty(ColorString))
                    {

                        products = products.Where(s => s.product_color.Contains(ColorString));
                    }
                    if (!String.IsNullOrEmpty(TakenString))
                    {
                        if(TakenString == "Taken")
                        {
                            products = products.Where(s => s.Taken.Equals(true));

                        }
                        else if (TakenString == "NotTaken")
                        {
                            products = products.Where(s => s.Taken.Equals(false));

                        }
                    }

                    return View(await products.ToListAsync());

                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }


        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
  
            try
            {
                if (IsAdminOrSupplier())
                {
                    if (id == null)
                    {
                        return RedirectToAction("Login", "Home");
                    }

                    var products = await _context.Products
                        .FirstOrDefaultAsync(m => m.product_Id == id);
                    if (products == null)
                    {
                        return RedirectToAction("Login", "Home");
                    }

                    ViewData["product_image"] = products.product_image;


                    return View(products);

                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Products/Create
        public IActionResult Create()
        {

            try
            {
                if (IsAdminOrSupplier())
                {
                    return View();


                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("product_Id,product_branch,product_name,product_description,product_category,Taken,TakenDate,buyer_name,product_image,product_color,ReleaseDate")] Products products, IFormFile product_image)
        {



            try
                {
                    if (IsAdminOrSupplier())
                    {
                    products.ReleaseDate = System.DateTime.Now;
                    //products.Taken = false;


                    if (ModelState.IsValid)
                    {


                        _context.Add(products);
                        await _context.SaveChangesAsync();



                        if (product_image != null)
                        {
                            if (product_image.Length != 0)
                            {
                                FileInfo fi = new FileInfo(product_image.FileName);
                                var newFilename = products.product_Id + "_" + String.Format("{0:d}", (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                                var webPath = hostingEnvironment.WebRootPath;
                                var path = Path.Combine("", webPath + @"\ImageFiles\" + newFilename);
                                var pathToSave = @"/ImageFiles/" + newFilename;
                                using (var stream = new FileStream(path, FileMode.Create))
                                {
                                    await product_image.CopyToAsync(stream);
                                }
                                products.product_image = pathToSave;
                                _context.Update(products);
                                await _context.SaveChangesAsync();
                            }
                        }


                        return RedirectToAction(nameof(Index));
                    }

                    return View(products);

                }
                    else
                    {
                        return RedirectToAction("Login", "Home");
                    }
                }
                catch
                {
                    return RedirectToAction("Login", "Home");
                }





         
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {




            try
            {
                if (IsAdminOrSupplier())
                {
                    if (id == null)
                    {
                        return RedirectToAction("Login", "Home");
                    }

                    var products = await _context.Products.FindAsync(id);
                    if (products == null)
                    {
                        return RedirectToAction("Login", "Home");
                    }

                    ViewData["product_image"] = products.product_image;

                    return View(products);

                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("product_Id,UID,product_branch,product_name,product_description,product_category,product_color,Taken,TakenDate,buyer_name,product_image,ReleaseDate")] Products products)
        {


            
            try
            {
                if (IsAdminOrSupplier())
                {
                    if (id != products.product_Id)
                    {
                        return RedirectToAction("Login", "Home");
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            if(products.Taken == false)
                            {
                                products.buyer_name = "";
                                products.TakenDate = "";
                                products.UID = null;

                            }
                            _context.Update(products);
                            await _context.SaveChangesAsync();

                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!ProductsExists(products.product_Id))
                            {
                                return RedirectToAction("Login", "Home");
                            }
                            else
                            {
                                throw;
                            }
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    return View(products);

                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
  

            try
            {
                if (IsAdminOrSupplier())
                {
                    if (id == null)
                    {
                        return RedirectToAction("Login", "Home");
                    }

                    var products = await _context.Products
                        .FirstOrDefaultAsync(m => m.product_Id == id);
                    if (products == null)
                    {
                        return RedirectToAction("Login", "Home");
                    }

                    return View(products);

                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }
        }


      

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            try
            {
                if (IsAdminOrSupplier())
                {
                    var products = await _context.Products.FindAsync(id);
                    _context.Products.Remove(products);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }


        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.product_Id == id);
        }

        private bool IsAdminOrSupplier()
        {

            if (HttpContext.Session.GetString("Role") == "Admin" || HttpContext.Session.GetString("Role") == "Supplier")
            {
                return true;
            }
            return false;

        }
        private Categories GetCategory(string CategoryString)
        {



            if (CategoryString.Contains("Automotive"))
            {
                return Categories.Automotive;
            }

            else if (CategoryString.Contains("Electronics"))
            {
                return Categories.Electronics;

            }

            else if (CategoryString.Contains("Entertainment"))
            {
                return Categories.Entertainment;
            }

            else if (CategoryString.Contains("Clothing"))
            {
                return Categories.Clothing;
            }

            else if (CategoryString.Contains("furniture"))
            {
                return Categories.furniture;
            }

            else if (CategoryString.Contains("kitchen"))
            {
                return Categories.kitchen;
            }

            else if (CategoryString.Contains("Food"))
            {
                return Categories.Food;
            }

            else if (CategoryString.Contains("Computers"))
            {
                return Categories.Computers;
            }

            else if (CategoryString.Contains("Home"))
            {
                return Categories.Home;
            }

            else if (CategoryString.Contains("Office"))
            {
                return Categories.Office;
            }

            else if (CategoryString.Contains("Sports"))
            {
                return Categories.Sports;
            }

            else if (CategoryString.Contains("Software"))
            {
                return Categories.Software;
            }

            else if (CategoryString.Contains("Health"))
            {
                return Categories.Health;
            }

            else if (CategoryString.Contains("Beauty"))
            {
                return Categories.Beauty;
            }

            else if (CategoryString.Contains("Arts"))
            {
                return Categories.Arts;
            }

            return Categories.Sports;

        }
    }
}
