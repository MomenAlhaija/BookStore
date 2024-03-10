using System;
using System.Web;
using System.Web.Mvc;

public class GlobalErrorAttribute : FilterAttribute, IExceptionFilter
{
    public void OnException(ExceptionContext filterContext)
    {
        
        filterContext.ExceptionHandled = true;

        int httpErrorCode = 500; 
        if (filterContext.Exception is HttpException httpException)
        {
            httpErrorCode = httpException.GetHttpCode();
        }
        var errorModel = new ErrorModel
        {
            ErrorMessage = filterContext.Exception.Message,
            HttpStatusCode = httpErrorCode 
        };

        filterContext.Controller.ViewData.Model = errorModel;

        filterContext.HttpContext.Response.StatusCode = httpErrorCode;

        filterContext.Result = new ViewResult
        {
            ViewName = "Error",
            ViewData = filterContext.Controller.ViewData
        };
    }
}
