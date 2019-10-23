using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Shop4U.Models;
using Microsoft.EntityFrameworkCore;


namespace Shop4U.Controllers
{
    public class ProductsGalleryController : Controller
    {

        private readonly Shop4UContext _context;

        public ProductsGalleryController(Shop4UContext context)
        {
            _context = context;
        }

        // GET: ProductsGallery/Index
        public async Task<IActionResult> Index(string product_category, string DescriptionString, string ColorString)
        {
            try
            {

                var products = from m in _context.Products select m;

                products = products.Where(s => s.Taken.Equals(false));



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

                return View(await products.ToListAsync());


            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }


        }


        // GET: ProductsGallery/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: ProductsGallery/Claim/5
        public async Task<IActionResult> Claim(int? id)
        {
            if (IsUser())
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



        // POST: ProductsGallery/Claim/5
        [HttpPost, ActionName("Claim")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClaimConfirmed(int? id)
        {
            if (IsUser())
            {
                if (id == null)
                {
                    return RedirectToAction("Login", "Home");
                }


                var products = await _context.Products.FindAsync(id);
                products.Taken = true;
                products.buyer_name = HttpContext.Session.GetString("Fullname");
                products.TakenDate = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
                products.UID = HttpContext.Session.GetInt32("UID");
                _context.Update(products);
                await _context.SaveChangesAsync();



                return RedirectToAction("ThankYouOrder", new { id = id });
            }
            else
            {
                return RedirectToAction("Login", "Home");

            }



        }

        // GET: ProductsGallery/ThankYouOrder/5
        public async Task<IActionResult> ThankYouOrder(int? id)
        {

            if (IsUser())
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



        // POST: ProductsGallery/DeleteOrder/5
        [HttpPost, ActionName("ThankYouOrder")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThankYouOrderConfirmed(int? id)
        {
            if (IsUser())
            {

                if (id == null)
                {
                    return RedirectToAction("Login", "Home");
                }


                var products = await _context.Products.FindAsync(id);
                products.Taken = false;
                products.buyer_name = "";
                products.TakenDate = "";
                products.UID = null;

                _context.Update(products);
                await _context.SaveChangesAsync();



                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                return RedirectToAction("Login", "Home");

            }



        }



        // GET: ProductsGallery/MyOrders
        public async Task<IActionResult> MyOrders(string product_category, string BranchString, string ColorString)
        {

            if (IsUser())
            {

                try
                {

                    var products = from m in _context.Products select m;

                    products = products.Where(s => s.UID.Equals(HttpContext.Session.GetInt32("UID")));


                    if (!String.IsNullOrEmpty(product_category))
                    {
                        products = products.Where(s => s.product_category == GetCategory(product_category));
                    }
                    if (!String.IsNullOrEmpty(BranchString))
                    {

                        products = products.Where(s => s.product_branch.Contains(BranchString));
                    }
                    if (!String.IsNullOrEmpty(ColorString))
                    {

                        products = products.Where(s => s.product_color.Contains(ColorString));
                    }

                    return View(await products.ToListAsync());


                }
                catch
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");

            }


        }


        private bool IsUser()
        {

            var id = HttpContext.Session.GetInt32("UID");

            if (id == null)
            {
                return false;
            }
            return true;

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