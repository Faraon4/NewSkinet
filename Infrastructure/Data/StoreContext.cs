using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands {get; set;}
        public DbSet<ProductType> ProductTypes { get; set; }


        // In this method we write the requirements for our database
        // Instead of writing it here , we created a Config folder , where we wrote the necesary configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
         base.OnModelCreating(modelBuilder);
         modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());   
        }

    }
}