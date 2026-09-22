using Microsoft.AspNetCore.Http;

namespace CService.Core.Middlewares;

public class OpeningPasswordMiddleware
{
    private readonly RequestDelegate _next;

    public OpeningPasswordMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        //context.Response.Cookies.Delete("OpeningPasswordVerified"); // TEST

        var path = context.Request.Path.Value ?? string.Empty;

        var isExempt = path.StartsWith("/Account", StringComparison.OrdinalIgnoreCase)
            || path.StartsWith("/css", StringComparison.OrdinalIgnoreCase)
            || path.StartsWith("/js", StringComparison.OrdinalIgnoreCase)
            || path.StartsWith("/lib", StringComparison.OrdinalIgnoreCase);

        if (context.User.Identity?.IsAuthenticated == true && !isExempt)
        {
            var verified = context.Request.Cookies["OpeningPasswordVerified"] == "true";
            if (!verified)
            {
                var returnUrl = Uri.EscapeDataString(path + context.Request.QueryString);
                context.Response.Redirect($"/Account/OpeningPassword?returnUrl={returnUrl}");
                return;
            }
        }

        await _next(context);
    }
}