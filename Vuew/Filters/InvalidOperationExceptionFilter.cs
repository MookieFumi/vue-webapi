using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Vuew.Filters
{
    public class InvalidOperationExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception as InvalidOperationException;

            if (exception == null)
            {
                return;
            }

            var result = new ObjectResult(new { Error = exception.Message })
            {
                StatusCode = (int)HttpStatusCode.Conflict
            };

            context.Result = result;
        }
    }
}
