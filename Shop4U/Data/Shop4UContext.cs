using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop4U.Models;

namespace Shop4U.Models
{
    public class Shop4UContext : DbContext
    {
        public Shop4UContext (DbContextOptions<Shop4UContext> options)
            : base(options)
        {
        }

        public DbSet<Shop4U.Models.Products> Products { get; set; }

        public DbSet<Shop4U.Models.Users> Users { get; set; }

        public DbSet<Shop4U.Models.Branches> Branches { get; set; }
    }
}
