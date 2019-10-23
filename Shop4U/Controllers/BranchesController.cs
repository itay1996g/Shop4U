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
    public class BranchesController : Controller
    {
        private readonly Shop4UContext _context;

        public BranchesController(Shop4UContext context)
        {
            _context = context;
        }

        // GET: Branches
        public async Task<IActionResult> Index(string searchString, string AddressString, string branch_ownerString)
        {

            try
            {
                if (IsAdminOrSupplier())
                {
                    var branches = from m in _context.Branches
                                select m;

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        branches = branches.Where(s => s.branch_name.Contains(searchString));
                    }
                    if (!String.IsNullOrEmpty(AddressString))
                    { 
                    
                        branches = branches.Where(s => s.branch_address.Contains(AddressString));
                    }
                    if (!String.IsNullOrEmpty(branch_ownerString))
                    { 
                    
                        branches = branches.Where(s => s.branch_owner.Contains(branch_ownerString));
                    }

                    return View(await branches.ToListAsync());

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

        // GET: Branches/Details/5
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

                    var branches = await _context.Branches
                        .FirstOrDefaultAsync(m => m.branch_Id == id);
                    if (branches == null)
                    {
                         return RedirectToAction("Login", "Home");
                    }

                    return View(branches);
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

        // GET: Branches/Create
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

        // POST: Branches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("branch_Id,branch_name,branch_address,branch_owner,branch_lat,branch_lng")] Branches branches)
        {
    


            try
            {
                if (IsAdminOrSupplier())
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(branches);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    return View(branches);
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

        // GET: Branches/Edit/5
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

                    var branches = await _context.Branches.FindAsync(id);
                    if (branches == null)
                    {
                         return RedirectToAction("Login", "Home");
                    }
                    return View(branches);
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

        // POST: Branches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("branch_Id,branch_name,branch_address,branch_owner,branch_lat,branch_lng")] Branches branches)
        {


            try
            {
                if (IsAdminOrSupplier())
                {
                    if (id != branches.branch_Id)
                    {
                         return RedirectToAction("Login", "Home");
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(branches);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!BranchesExists(branches.branch_Id))
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
                    return View(branches);
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

        // GET: Branches/Delete/5
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

                    var branches = await _context.Branches
                        .FirstOrDefaultAsync(m => m.branch_Id == id);
                    if (branches == null)
                    {
                         return RedirectToAction("Login", "Home");
                    }

                    return View(branches);
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

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {



            try
            {
                if (IsAdminOrSupplier())
                {
                    var branches = await _context.Branches.FindAsync(id);
                    _context.Branches.Remove(branches);
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

        private bool BranchesExists(int id)
        {
            return _context.Branches.Any(e => e.branch_Id == id);

        }


        private bool IsAdmin()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                return true;
            }
            return false;

        }
        private bool IsAdminOrSupplier()
        {
            if (HttpContext.Session.GetString("Role") == "Admin" || HttpContext.Session.GetString("Role") == "Supplier")
            {
                return true;
            }
            return false;

        }
    }
}
