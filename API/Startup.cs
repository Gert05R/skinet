using API.Extensions;
using API.Helpers;
using API.Middleware;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

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

            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            //lambda expression
            services.AddDbContext<StoreContext> (x => 
            x.UseSqlite(_config.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppIdentityDbContext> (x => 
            {
                x.UseSqlite(_config.GetConnectionString("IdentityConnection"));
            }
            );

            services.AddSingleton<IConnectionMultiplexer>(c => {
                var configuration= ConfigurationOptions.Parse(_config.GetConnectionString("Redis"),
                true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddApplicationServices();
            services.AddIdentityServices(_config);
            services.AddSwaggerDocumentation();
            services.AddCors(opt => 
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

            
        }

        //add middleware when a http request comes in
        //order is important for adding middleware
        //Little bits of software
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();


            if (env.IsDevelopment())
            {
                //error handling
                //app.UseDeveloperExceptionPage();
               
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            //redirects automatically to HTTPS if we accidentally trigger http
            app.UseHttpsRedirection();
            // in order for us to access our api controller endpoints, they have to be routed to those endpoints via this midlleware
            app.UseRouting();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            //should always come before auth

            app.UseAuthentication();

            //identity and auth
            app.UseAuthorization();

            app.UseSwaggerDocumentation();

            //this is how our app knows which endpoints are available so they can be routed to
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
