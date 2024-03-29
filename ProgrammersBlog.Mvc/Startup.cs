﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgrammersBlog.Services.AutoMapper.Profiles;
using ProgrammersBlog.Services.Extensions;

namespace ProgrammersBlog.Mvc
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(opt =>
            {
                //enum donusturucu. enum degerlerimiz json formatý icide islenmis olucak.
                //enum deðerlerini string olarak almak istersem view icinde if(ajaxModel.ResultStatus=="success") gibi bir sey yapýucaksam
                //(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)); yapmam gerekiyor.
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                //nested yani iç içe olan objelerde bu objeler birbirini referrans ettiklerinde sorun yasamadan cevirmemizi saglýyor.  
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
            services.AddAutoMapper(typeof(CategoryProfile),typeof(ArticleProfile));
            services.LoadMyServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //hatayı sayfaya basıcak
                app.UseStatusCodePages();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
