using CursoOnline.Dominio.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class UnitOfWorkMiddleware
{
    private readonly RequestDelegate _next;

    public UnitOfWorkMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        var uow = (IUnitOfWork)context.RequestServices.GetService(typeof(IUnitOfWork));
        await uow.Commit();
    }
}

public static class UnitOfWorkMiddlewareExtension
{
    public static IApplicationBuilder UseUnitOfWorkMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UnitOfWorkMiddleware>();
    }
}