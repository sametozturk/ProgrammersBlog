using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete;
using ProgrammersBlog.Data.Concrete.EntityFramework.Contexts;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Concrete;

namespace ProgrammersBlog.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<ProgrammersBlogContext>();
            //scoped : her request'te nesne tekrar olusur ve bir request içerisinde sadece bir nesne kullanılır.
            //transient ve scoped farkı : transient her nesne cagrımında yeni bir instance olusturur. scoped ise request esnasında yeni instance olusturur
            //ve request sonlanana kadar aynı nesne kullanılır.
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();
            return serviceCollection;
        }
    }
}
