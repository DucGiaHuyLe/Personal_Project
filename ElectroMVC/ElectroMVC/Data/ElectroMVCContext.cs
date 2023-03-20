using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ElectroMVC.Models;

namespace ElectroMVC.Data
{
    public class ElectroMVCContext : DbContext
    {
        public ElectroMVCContext (DbContextOptions<ElectroMVCContext> options)
            : base(options)
        {
        }

        public DbSet<ElectroMVC.Models.User> User { get; set; } = default!;

        public DbSet<ElectroMVC.Models.Brand> Brand { get; set; }

        public DbSet<ElectroMVC.Models.Category> Category { get; set; }

        public DbSet<ElectroMVC.Models.Product> Product { get; set; }
		public DbSet<ElectroMVC.Models.Cart> Cart { get; set; }
	}
}
