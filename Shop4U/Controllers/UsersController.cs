using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop4U.Models;

namespace Shop4U.Controllers
{
    public class UsersController : Controller
    {
        private readonly Shop4UContext _context;

        public UsersController(Shop4UContext context)
        {
            _context = context;
        }

        
     

        // GET: Users
        public async Task<IActionResult> Index(string EmaileString, string FullnameString, string RoleString)
        {


            try
            {
                if (IsAdmin())
                {
                    var users = from m in _context.Users
                                select m;

                    if (!String.IsNullOrEmpty(EmaileString))
                    {
                        users = users.Where(s => s.user_email.Contains(EmaileString));
                    }
                    if (!String.IsNullOrEmpty(FullnameString))
                    {
                        users = users.Where(s => s.user_fullname.Contains(FullnameString));
                    }
                    if (!String.IsNullOrEmpty(RoleString))
                    {
                        users = users.Where(s => s.Role.Contains(RoleString));
                    }

                    return View(await users.ToListAsync());
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

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            try
            {
                if (IsAdmin())
                {
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

        // GET: Users/Create
        public IActionResult Create()
        {

            try
            {
                if (IsAdmin())
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

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UID,user_email,user_fullname,user_password,user_phone,user_city,Role,user_gender,birth_year")] Users users)
        {

            try
            {
                if (IsAdmin())
                {
                    users.CreatedDate = System.DateTime.Now;
                    if (ModelState.IsValid)
                    {
                        _context.Add(users);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    return View(users);
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

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            try
            {
                if (IsAdmin())
                {
                    if (id == null)
                    {
                        return RedirectToAction("Login", "Home");
                    }

                    var users = await _context.Users.FindAsync(id);
                    if (users == null)
                    {
                        return RedirectToAction("Login", "Home");
                    }
                    return View(users);
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

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UID,user_email,user_fullname,user_password,user_phone,user_city,CreatedDate,Role,user_gender,birth_year")] Users users)
        {



            try
            {
                if (IsAdmin())
                {

                    if (id != users.UID)
                    {
                        return RedirectToAction("Login", "Home");
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(users);
                            await _context.SaveChangesAsync();
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
                        return RedirectToAction(nameof(Index));
                    }
                    return View(users);

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

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            try
            {
                if (IsAdmin())
                {
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

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (IsAdmin())
                {
                    //Erase UID of buyer


                    // _context.Update(users);
                    //await _context.SaveChangesAsync();
                    var personsToUpdate = _context.Products.Where(o => o.UID == id);
                    foreach (Products p in personsToUpdate)
                    {
                        p.UID = null;
                           
                    }

                    _context.SaveChanges();



                    var users = await _context.Users.FindAsync(id);
                   // var product = await _context.Products.FindAsync(users.UID);

                    _context.Users.Remove(users);
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

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UID == id);
        }
        private bool IsAdmin()
        {

            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                return true;
            }
            return false;

        }
    }
}
