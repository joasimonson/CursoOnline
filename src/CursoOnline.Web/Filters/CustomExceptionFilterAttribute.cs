using CursoOnline.Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CursoOnline.Web.Filters
{
    public sealed class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            bool isAjaxCall = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";

            if (isAjaxCall)
            {
                var domainExcept = context.Exception is RegraDominioException;

                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = domainExcept ? 502 : 500;

                context.Result = context.Exception is RegraDominioException dominio ?
                    new JsonResult(dominio?.Exceptions) :
                    new JsonResult("An error ocorred");
                
                context.ExceptionHandled = true;
            }

            base.OnException(context);
        }
    }
}
