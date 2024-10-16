﻿using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Kutuphane.Core.DataAccess;
using Kutuphane.Core.DataAccess.EntityFramework;
using Kutuphane.DataAccess.Concrete.EntityFramework;
using Kutuphane.WebUI.Models.Author;
using Kutuphane.WebUI.Models.Book;
using Kutuphane.WebUI.Models.Category;
using Kutuphane.WebUI.Models.Operations;
using Kutuphane.WebUI.Models.User;
using Kutuphane.WebUI.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUI
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
            services.AddRazorPages();

            services.AddControllersWithViews();

            services.AddScoped(typeof(IQueryableRepository<>), typeof(EfQueryableRepository<>));
            services.AddScoped<DbContext, Context>();


            // Notyf Alanı 
            services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
            services.AddToastify(config => { config.DurationInSeconds = 1000; config.Position = Position.Right; config.Gravity = Gravity.Top; });

            //Authentication


            //FluentValidation Çağırma işlemi
            services.AddMvc(setup =>
            {
                //...mvc setup...
            }).AddFluentValidation();

            //Validasyonlar
            services.AddTransient<IValidator<BookAddModel>, BookAddValidation>();
            services.AddTransient<IValidator<UserAddModel>, UserAddValidation>();
            services.AddTransient<IValidator<BookGiveModel>, BookGiveValidation>();
            services.AddTransient<IValidator<BookEditModel>, BookEditValidation>();
            services.AddTransient<IValidator<CategoryAddModel>, CategoryAddValidation>();
            services.AddTransient<IValidator<AuthorAddModel>, AuthorAddValidation>();
            services.AddTransient<IValidator<UserEditModel>, UserEditValidation>();

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseNotyf();

            DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("Login.cshtml");
            //Setting the Default Files
            app.UseDefaultFiles(defaultFilesOptions);
            //Adding Static Files Middleware to serve the static files
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Admin}/{action=Login}/{id?}");
            });
        }
    }
}
