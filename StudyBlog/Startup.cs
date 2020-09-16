using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudyBlog.Models;
using StudyBlog.Services;

namespace StudyBlog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddTransient<UploadFileService>();
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BlogContext>(options =>
                        options.UseLazyLoadingProxies()
                        .UseNpgsql(connection))
                .AddIdentity<User, IdentityRole>(
                    options =>
                    {
                        options.Password.RequireDigit = false; //Требуются ли цифры
                        options.Password.RequiredLength = 5; //Требуемая длина пароля
                        options.Password.RequireLowercase = false; // Требуются ли символы в нижнем регистре
                        options.Password.RequireUppercase = false; //Требуются ли символы в верхнем регистре
                        options.Password.RequiredUniqueChars = 1; //Количество уникальных символов в пароле
                        options.Password.RequireNonAlphanumeric = false; //Требуются ли неалфавитно-цифровые символы 
                    })
                .AddEntityFrameworkStores<BlogContext>();
            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStatusCodePagesWithRedirects("/Errors/?code={0}");
            app.UseRouting();

            
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Posts}/{action=Index}/{id?}");
            });
        }
    }
}