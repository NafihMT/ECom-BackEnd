using ECom.Application.Interfaces.Services;
using ECom.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECom.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRegisterService, RegisterService>();
        return services;
    }
}
