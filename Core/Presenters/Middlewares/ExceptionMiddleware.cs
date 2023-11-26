using Core.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace Core.Presenters.Middlewares
{
    public class ExceptionMiddleware : IExceptionFilter, IFilterMetadata
    {
        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;

            if (exception is InvalidCredentialException)
            {
                BadRequestObjectResult res = new(exception.Message)
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };

                context.Result = res;
                return;
            }

            if (exception is NotFoundException)
            {
                BadRequestObjectResult res = new(exception.Message)
                {
                    StatusCode = StatusCodes.Status404NotFound
                };

                context.Result = res;
                return;
            }

            if (exception is BadRequestException)
            {
                BadRequestObjectResult res = new(exception.Message)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
                context.Result = res;
                return;
            }

            BadRequestObjectResult badrequest = new(exception.Message)
            {
                StatusCode = 500
            };

            context.Result = badrequest;
        }
    }
}
