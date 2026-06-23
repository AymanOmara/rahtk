using FluentValidation;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Rahtk.Application.Features;
using Rahtk.Application.Features.Address.Service;
using Rahtk.Application.Features.Category;
using Rahtk.Application.Features.Order;
using Rahtk.Application.Features.Payment.Service;
using Rahtk.Application.Features.Product;
using Rahtk.Application.Features.Product.Service;
using Rahtk.Application.Features.Reminder.Service;
using Rahtk.Application.Features.User;

namespace Rahtk.Application.Common
{
    public static class Extentions
    {
        public static void RegisterApplicationDependancies(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IAddressService, AddressService>();

            services.AddScoped<IPaymentService, PaymentService>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IReminderService, ReminderService>();
        }
    }
}