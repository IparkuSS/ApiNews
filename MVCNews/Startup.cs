using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MVCNews.Extensions;
using MVCNews.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCNews
{
    /// <>
    /// 
    /// 
    /// </summary>
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


            /*  services.ConfigureApplicationCookie(options =>
              {
                  options.Cookie.Name = "myCompanyAuth";
                  options.Cookie.HttpOnly = true;
                  options.LoginPath = "/account/login";
                  options.AccessDeniedPath = "/account/accessdenied";
                  options.SlidingExpiration = true;
              });*/
            /*services.AddAuthorization(x =>
            {
                x.AddPolicy("AdministratorArea", policy => { policy.RequireRole("Administrator"); });

                x.AddPolicy("RedactorArea", policy => { policy.RequireRole("Redactor"); });
            });*/
            /* services.AddControllersWithViews(x =>
             {
                 x.Conventions.Add(new AreaAuthorization("Administrator", "AdministratorArea"));
                 x.Conventions.Add(new AreaAuthorization("Redactor", "RedactorArea"));
             });*/

            services.ConfigureCookie();
            services.ConfigureAuthorization();
            services.ConfigureutControllersWithViews();





            services.AddSession();
            services.AddControllersWithViews();
            services.ConfigureutJwt(Configuration);
            /*  var jwtSettings = Configuration.GetSection("JwtSettings");
              var secretKey = Environment.GetEnvironmentVariable("SECRET");

              services.AddAuthentication(opt =>
              {
                  opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              }).AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,

                      ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                      ValidAudience = jwtSettings.GetSection("validAudience").Value,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                  };
              });*/



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
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseSession();
            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                name: "Redactor",
                areaName: "Redactor",
                pattern: "Redactor/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                name: "Administrator",
                areaName: "Administrator",
                pattern: "Administrator/{controller=Home}/{action=Index}/{id?}");





                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");




            });
        }
    }
}
