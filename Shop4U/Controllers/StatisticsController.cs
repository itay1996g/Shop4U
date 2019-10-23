using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop4U.Models;


namespace Shop4U.Controllers
{
    public class StatisticsController : Controller
    {

        private readonly Shop4UContext _context;

        public StatisticsController(Shop4UContext context)
        {
            _context = context;
        }

        public IActionResult Index()
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


        public JsonResult GetCountByBranch()
        {




            try
            {
                if (IsAdmin())
                {
                    var query = _context.Products
                           .GroupBy(p => p.product_branch)
                           .Select(g => new { name = g.Key, count = g.Count() });

                    return Json(query);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public JsonResult GetCountByCategory()
        {

            try
            {
                if (IsAdmin())
                {


                    var query = _context.Products
                           .GroupBy(p => p.product_category)
                           .Select(g => new { name = g.Key, count = g.Count() });

                    return Json(query);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }




        public JsonResult GetCountByCityUsers()
        {

            try
            {
                if (IsAdmin())
                {


                    /*
                     SELECT f.user_city, COUNT(d.product_Id)
                        FROM Products d
                        JOIN Users f
                        ON d.UID = f.UID
                        WHERE d.Taken = true
                        GROUP BY f.user_city                  
                     */


                    var query = from d in _context.Products
                                    join f in _context.Users
                                    on d.UID equals f.UID
                                    where (d.Taken == true)
                                    select new { f.user_city, d.product_Id } into x
                                    group x by new { x.user_city } into g
                                    select new
                                    {
                                        Location = g.Key.user_city,
                                        Count = g.Select(x => x.product_Id).Count()
                                    };

                    return Json(query);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }



        public JsonResult GetCountByUsersGender()
        {

            try
            {
                if (IsAdmin())
                {


                    /*
                     SELECT f.user_gender, COUNT(d.product_Id)
                        FROM Products d
                        JOIN Users f
                        ON d.UID = f.UID
                        WHERE d.Taken = true
                        GROUP BY f.user_gender                  
                     */


                    var query = from d in _context.Products
                                join f in _context.Users
                                on d.UID equals f.UID
                                where (d.Taken == true)
                                select new { f.user_gender, d.product_Id } into x
                                group x by new { x.user_gender } into g
                                select new
                                {
                                    gender = g.Key.user_gender,
                                    Count = g.Select(x => x.product_Id).Count()
                                };

                    return Json(query);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

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




