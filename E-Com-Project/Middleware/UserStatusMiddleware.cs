using System.Security.Claims;
using ECom.Domain.Interfaces.Repositories;

namespace ECom.Api.Middleware;

public class UserStatusMiddleware
{
    private readonly RequestDelegate _next;

    public UserStatusMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserRepository userRepository)
    {
        try
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                  ?? context.User.FindFirst("nameid")?.Value
                                  ?? context.User.FindFirst("sub")?.Value
                                  ?? context.User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;

                if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out int userId))
                {
                    var user = await userRepository.GetByIdAsync(userId);

                    if (user == null || user.IsBlocked)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync("{\"message\": \"Your account has been blocked by an administrator.\"}");
                        return; 
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in UserStatusMiddleware: {ex.Message}");
        }

        await _next(context);
    }
}