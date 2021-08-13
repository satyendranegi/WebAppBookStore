using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAppBookStore.Data;
using WebAppBookStore.Repository;

namespace WebAppBookStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(
                options=>options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<BookStoreContext>();
            services.AddControllersWithViews();
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
#endif
            services.AddScoped<BookRepository, BookRepository>();
        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //middleware
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("hello from my first middleware");//#1
            //    await next();
            //    await context.Response.WriteAsync("hello from my first middleware response");// then #2s
            //});
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("hello from my second middleware"); // then #2
            //    await next();
            //    await context.Response.WriteAsync("hello from my second middleware"); //#1
            //});

            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
            //    RequestPath = "/MyStaticFiles"
            //});
            //Routing, map the all request of resource
            app.UseRouting();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapGet("/", async context =>
                //{
                //    if (env.IsDevelopment())
                //    {
                //       await context.Response.WriteAsync("Hello from Dev");
                //    }
                //    else if (env.IsProduction())
                //    {
                //        await context.Response.WriteAsync("Hello from Pro");
                //    }
                //    else if (env.IsEnvironment("Develop"))//custom environment
                //    {
                //        await context.Response.WriteAsync("Hello from Custom environment");
                //    }
                //    else
                //    {
                //        await context.Response.WriteAsync(env.EnvironmentName);
                //    }
                //});
            });
            
        }
    }
}
