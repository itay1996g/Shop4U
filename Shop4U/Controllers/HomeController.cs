using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop4U.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Shop4UML.Model;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shop4U.Controllers
{
    public class HomeController : Controller
    {

        private readonly Shop4UContext _context;

        public HomeController(Shop4UContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult RegisterSuccess()
        {
            return View();
        }


        // GET: Home/Register

        public IActionResult Register()
        {
            return View();
        }



        // POST: Home/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UID,user_email,user_fullname,user_password,user_phone,user_city,Role,user_gender,birth_year")] Users users)
        {
            if (ModelState.IsValid)
            {

                users.CreatedDate = System.DateTime.Now;
                users.Role = "User";
                _context.Add(users);
                await _context.SaveChangesAsync();
                //HttpContext.Session.SetString("Role", users.Role.ToString());
                //HttpContext.Session.SetString("Fullname", users.user_fullname.ToString());
                //HttpContext.Session.SetInt32("UID", users.UID);
                //HttpContext.Session.SetString("Role", users.Role.ToString());



                return RedirectToAction(nameof(RegisterSuccess));

            }
            return View(users);
        }

        // GET : Home/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST : Home/Login

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind("user_email,user_password")] Users users)
        {

            var record = _context.Users.FirstOrDefault(u => u.user_email == users.user_email && u.user_password == users.user_password);
            if (record != null)
            {

                HttpContext.Session.SetString("Role", record.Role.ToString());
                HttpContext.Session.SetString("Fullname", record.user_fullname.ToString());
                HttpContext.Session.SetInt32("UID", record.UID);
                // Add input data
                var input = new ModelInput();
                input.Birth_year = record.birth_year;
                if (record.user_gender == Gender.זכר)
                    input.User_gender = 1;
                else
                    input.User_gender = 2;

                // Load model and predict output of sample data
                ModelOutput result = ConsumeModel.Predict(input);
                HttpContext.Session.SetString("ML_Category", GetCategory(int.Parse(result.Prediction.ToString())));




                return RedirectToAction("PersonalDetails", "Account");
            }
            else
            {
                ModelState.AddModelError("CustomError", "כתובת מייל או סיסמא שגויים.");

            }


            return View();
        }


        // GET : Home/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Contact()
        {

       IEnumerable<SelectListItem> items = _context.Branches.Select(c => new SelectListItem
                  {
                      Value = c.branch_address,
                      Text = c.branch_name
 
                  });
            ViewBag.branches_list = items;


            return View();
        }


        public JsonResult GeBranches()
        {

            try
            {

                var query = _context.Branches.Select(g => new { lat = g.branch_lat, lng = g.branch_lng });
                return Json(query);

            }
            catch
            {
                return null;
            }

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string GetCategory(int CategoryString)
        {



            if (CategoryString == 1)
            {
                return "Automotive";
            }

            else if (CategoryString == 2)
            {
                return "Electronics";

            }

            else if (CategoryString == 3)
            {
                return "Entertainment";
            }

            else if (CategoryString == 4)
            {
                return "Clothing";
            }

            else if (CategoryString == 5)
            {
                return "furniture";
            }

            else if (CategoryString == 6)
            {
                return "kitchen";
            }

            else if (CategoryString == 7)
            {
                return "Food";
            }

            else if (CategoryString == 8)
            {
                return "Computers";
            }

            else if (CategoryString == 9)
            {
                return "Home";
            }

            else if (CategoryString == 10)
            {
                return "Office";
            }

            else if (CategoryString == 11)
            {
                return "Sports";
            }

            else if (CategoryString == 12)
            {
                return "Software";
            }

            else if (CategoryString == 13)
            {
                return "Health";
            }

            else if (CategoryString == 14)
            {
                return "Beauty";
            }

            else if (CategoryString == 15)
            {
                return "Arts";
            }

            return "Sports";

        }
    }
}








