using Microsoft.AspNetCore.Http.HttpResults;

namespace Middleware.MiddlewareGetHeader;
public class MiddlewareVerifyToken
{
    private readonly RequestDelegate _next;

    public MiddlewareVerifyToken(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers.Authorization.ToString();
        string[] tokenSeparator = authorizationHeader.Split(" ");
        if(tokenSeparator[1].Length < 16)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.WriteAsync("Token not found");
            return;
        }
        
        await _next(context);
    }
}
