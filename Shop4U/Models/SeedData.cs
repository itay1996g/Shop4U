using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Shop4U.Models
{

    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Shop4UContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<Shop4UContext>>()))
            {
                // Look for any Productss.


                bool update = false;




                
                //Init Branches
                if (!context.Branches.Any())
                {
                    update = true;

                    context.Branches.AddRange(
                    new Branches
                    {
                        branch_name = "ירושלים",
                        branch_address = "ירושלים",
                        branch_owner = "ברוך כהן",
                        branch_lat = 31.771849,
                        branch_lng = 35.217010


                    },
                    new Branches
                    {
                        branch_name = "פתח תקווה",
                        branch_address = "פתח תקווה",
                        branch_owner = "אלי דוד",
                        branch_lat = 32.0840,
                        branch_lng = 34.8878
                    },
                    new Branches
                    {
                        branch_name = "אילת",
                        branch_address = "אילת",
                        branch_owner = "נועם נתן",
                        branch_lat = 29.598301,
                        branch_lng = 34.972735
                    },
                    new Branches
                    {
                        branch_name = "ראשון לציון",
                        branch_address = "ראשון לציון",
                        branch_owner = "עומרי בר",
                        branch_lat = 31.969276,
                        branch_lng = 34.810663
                    }

                );
                }
                
                
                if (!context.Users.Any())
                {
                    update = true;

                    //Init Users
                    context.Users.AddRange(
                    new Users
                    {
                        user_email = "admin@gmail.com",
                        user_fullname = "עמוס יונה",
                        user_password = "123",
                        user_phone = "0524704055",
                        user_city = "פתח תקווה",
                        CreatedDate = DateTime.Parse("Oct 24, 2019"),
                        Role = "Admin",
                        birth_year = 1995,
                        user_gender = Gender.זכר
                    },
                   new Users
                   {
                       user_email = "user@gmail.com",
                       user_fullname = "יונתן חיים",
                       user_password = "123",
                       user_phone = "0522600187",
                       user_city = "ירושלים",
                       CreatedDate = DateTime.Parse("Oct 24, 2019"),
                       Role = "User",
                       birth_year = 1994,
                       user_gender = Gender.זכר
                   },
                   new Users
                   {
                       user_email = "supplier@gmail.com",
                       user_fullname = "יולי מני",
                       user_password = "123",
                       user_phone = "0522788965",
                       user_city = "ראשון לציון",
                       CreatedDate = DateTime.Parse("Oct 24, 2019"),
                       Role = "Supplier",
                       birth_year = 1993,
                       user_gender = Gender.נקבה
                   }

                );
                }
                
    
                
                
                if (!context.Products.Any())
                {
                    update = true;
                    context.Products.AddRange(

                        new Products
                        {
                            product_branch = "ירושלים",
                            product_name = "שולחן אוכל",
                            product_description = "שולחן מאוד יפה לפינת אוכל",
                            product_category = Categories.kitchen,
                            product_image = "/ImageFiles/table.jpg",
                            product_color = "חום",
                            ReleaseDate = DateTime.Parse("Sep 01, 2019")
                        },
                        new Products
                        {
                            product_branch = "פתח תקווה",
                            product_name = "כיסא",
                            product_description = "כיסא נוח ומתאים לכל סוגי הבתים",
                            product_category = Categories.furniture,
                            product_image = "/ImageFiles/chair.jpg",
                            product_color = "לבן",
                            ReleaseDate = DateTime.Parse("Sep 01, 2019")
                        },
                        new Products
                        {
                            product_branch = "אילת",
                            product_name = "כדור",
                            product_description = "כדור מעולה מתאים לכל משחק",
                            product_category = Categories.Sports,
                            product_image = "/ImageFiles/ball.jpg",
                            product_color = "כתום",
                            ReleaseDate = DateTime.Parse("Sep 01, 2019")
                        },
                        new Products
                        {
                            product_branch = "ראשון לציון",
                            product_name = "ספה משרדית",
                            product_description = "ספה גדולה ונוחה שמתאימה למשרד",
                            product_category = Categories.Office,
                            product_image = "/ImageFiles/sofa.jpg",
                            product_color = "ירוק",
                            ReleaseDate = DateTime.Parse("Sep 01, 2019")
                        }



                    );
                }

                if (update)
                {
                    context.SaveChanges();

                }
                else
                {
                    return;
                }

            }
        }
    }
}







