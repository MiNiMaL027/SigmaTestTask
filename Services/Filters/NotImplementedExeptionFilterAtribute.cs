using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Services.Filters
{
    public class NotImplExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            if (context.Exception is NotFoundException)
            {
                context.Result = new NotFoundObjectResult(new
                {
                    errorMessage = context.Exception.Message
                });
            }
            else if (context.Exception is ValidationException)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    errorMessage = context.Exception.Message
                });
            }
            else if (context.Exception is AlreadyExistException)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    errorMessage = context.Exception.Message
                });
            }
            else if (context.Exception is ArgumentException)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    errorMessage = context.Exception.Message
                });
            }
            else if (context.Exception is InvalidOperationException)
            {
                context.Result = new ObjectResult(new
                {
                    errorMessage = context.Exception.Message
                })
                {
                    StatusCode = 500
                };
            }
            else
            {
                context.Result = new ObjectResult(new
                {
                    errorMessage = "An unexpected error occurred.",
                    details = context.Exception.Message
                })
                {
                    StatusCode = 500
                };
            }

            context.ExceptionHandled = true;
        }
    }
}
