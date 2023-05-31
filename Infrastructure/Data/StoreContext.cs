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



        // This part of the code is here because of the decimal problem in the SqlLite
        // In SqlLite , there is no decimal , so we change it into the double where we do have decimal
        if(Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            foreach(var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                foreach(var property in properties)
                {
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                }
            }            
        }

        }

    }
}