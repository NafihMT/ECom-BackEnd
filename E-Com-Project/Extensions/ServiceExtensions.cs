using ECom.Application.Interfaces.Services;
using ECom.Application.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace ECom.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IWishlistService, WishlistService>();

        return services;
    }
}
