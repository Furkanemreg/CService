using Microsoft.AspNetCore.Http;

namespace CService.Core.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var originalPath = context.Request.Path.Value;

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (context.Response.HasStarted)
                throw;

            context.Items["UnhandledException"] = ex;
            context.Items["ErrorRequestPath"] = originalPath;

            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Request.Path = "/Home/Error";

            await _next(context);
            return;
        }

        if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
        {
            context.Items["ErrorRequestPath"] = originalPath;
            context.Request.Path = "/Home/Error404";
            await _next(context);
        }
    }
}