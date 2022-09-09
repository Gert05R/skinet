using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext //https://www.udemy.com/course/learn-to-build-an-e-commerce-app-with-net-core-and-angular/learn/lecture/18136696#questions
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        //name of the table when the DB is created. Allows us to access the DB and use some of the methods
        public DbSet<Product> Products {get;set;}
        
        //https://www.udemy.com/course/learn-to-build-an-e-commerce-app-with-net-core-and-angular/learn/lecture/18136756#notes
        public DbSet<ProductBrand> ProductBrands {get;set;}

        public DbSet<ProductType> ProductTypes {get;set;}

        //OnmodelCreating is the method responsible for creating the migration
        protected override void OnModelCreating(ModelBuilder modelbuilder) 
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}