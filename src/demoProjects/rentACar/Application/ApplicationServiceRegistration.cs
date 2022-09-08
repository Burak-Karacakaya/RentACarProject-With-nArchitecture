using Application.Features.Brands.Rules;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Application katmanıyla ilgili Injection'ları yapar
            //Bütün Assembly tarar.

            services.AddAutoMapper(Assembly.GetExecutingAssembly()); // Automapper'ı ekler
            services.AddMediatR(Assembly.GetExecutingAssembly()); // MediaTr'ı ekler 

            services.AddScoped<BrandBusinessRules>(); // Business Rule'ları tek seferde oluşturup hep onu kullanıyoruz.AddScope sayesinde- Her farklı sınıf business rles2lar için ekleyeceğiz!

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>)); // Role bazlı yetkilendirme
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;

        }
    }
}
