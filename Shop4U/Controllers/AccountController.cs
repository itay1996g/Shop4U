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
    public class AccountController : Controller
    {

        private readonly Shop4UContext _context;

        public AccountController(Shop4UContext context)
        {
            _context = context;
        }

        // GET: Account/PersonalDetails
        public async Task<IActionResult> PersonalDetails()
        {
            var id = HttpContext.Session.GetInt32("UID");


            if (id == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UID == id);
            if (users == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View(users);
        }




        // GET: Account/PersonalDetailsEdit
        public async Task<IActionResult> PersonalDetailsEdit()
        {
            var id = HttpContext.Session.GetInt32("UID");


            if (id == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UID == id);
            if (users == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View(users);
        }



        // POST: Account/PersonalDetailsEdit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PersonalDetailsEdit([Bind("UID,user_email,user_password,user_fullname,user_phone,user_city,Role,user_gender,CreatedDate,birth_year")] Users users)
        {

                                 
            var id = HttpContext.Session.GetInt32("UID");

            if (id == null || id != users.UID)
            {
                return RedirectToAction("Login", "Home");
            }

            
            if(HttpContext.Session.GetString("Role") == "User")
            {

                users.Role = "User";

            }


            if (HttpContext.Session.GetString("Role") == "Supplier")
            {

                users.Role = "Supplier";

            }

            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                users.Role = "Admin";

            }



            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("PersonalDetails", "Account");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.UID))
                    {
                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(users);
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UID == id);
        }









    }
}