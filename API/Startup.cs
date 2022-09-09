using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;

          public Startup(IConfiguration config)
        {
            _config = config;
            
        }


        //https://www.udemy.com/course/learn-to-build-an-e-commerce-app-with-net-core-and-angular/learn/lecture/18136698#questions
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddControllers();
            //lambda expression
            services.AddDbContext<StoreContext> (x => 
            x.UseSqlite(_config.GetConnectionString("DefaultConnection")));
            
        }

        //add middleware when a http request comes in
        //order is important for adding middleware
        //Little bits of software
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //error handling
                app.UseDeveloperExceptionPage();
               
            }

            //redirects automatically to HTTPS if we accidentally trigger http
            app.UseHttpsRedirection();
            // in order for us to access our api controller endpoints, they have to be routed to those endpoints via this midlleware
            app.UseRouting();
            //identity and auth
            app.UseAuthorization();
            //this is how our app knows which endpoints are available so they can be routed to
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
