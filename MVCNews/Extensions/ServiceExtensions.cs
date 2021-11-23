﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MVCNews.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCNews.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCookie(this IServiceCollection services)
        {

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

        }

        public static void ConfigureAuthorization(this IServiceCollection services)
        {

            services.AddAuthorization(x =>
            {
                x.AddPolicy("AdministratorArea", policy => { policy.RequireRole("Administrator"); });

                x.AddPolicy("RedactorArea", policy => { policy.RequireRole("Redactor"); });
            });

        }

        public static void ConfigureutControllersWithViews(this IServiceCollection services)
        {


            services.AddControllersWithViews(x =>
            {
                x.Conventions.Add(new AreaAuthorization("Administrator", "AdministratorArea"));
                x.Conventions.Add(new AreaAuthorization("Redactor", "RedactorArea"));
            });

        }
        public static void ConfigureutJwt(this IServiceCollection services, IConfiguration configuration)
        {


            var jwtSettings = configuration.GetSection("JwtSettings");
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
            });

        }




    }
}
