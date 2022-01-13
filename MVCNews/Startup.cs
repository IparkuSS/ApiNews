using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using News.MVC.Extensions;
using News.MVC.Helper;
using News.MVC.Helper.Contracts;
using System.Text;
namespace News.MVC
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


            services.ConfigureCookie();
            services.ConfigureAuthorization();
            services.ConfigureutControllersWithViews();
            services.AddSession();
            services.AddControllersWithViews();

            ClientConfig clientConfig = Configuration.GetSection("ClientConfig").Get<ClientConfig>();
            services.AddSingleton(clientConfig);

            services.ConfigureutJwt(Configuration);
            services.ConfigureServicesManager();
            services.AddTransient<IApiModels, ApiModels>();
            services.AddTransient<IClientSection, ClientSection>();
            services.AddTransient<IClientSubsection, ClientSubsection>();
            services.AddTransient<IClientArticle, ClientArticle>();
            services.AddTransient<IClientAuthor, ClientAuthor>();

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
