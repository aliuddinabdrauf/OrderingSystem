using OrderingSystem.Infrastructure;
using OrderingSystem.Infrastructure.Dtos;
using System.Net;
using OrderingSystem.Application.Utils;

namespace OrderingSystem.WebApi.Middlewares
{
    public class ErrorHandlerMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            if (exception is RecordNotFoundException or NoDataUpdatedException) code = HttpStatusCode.NotFound;
            else if (exception is ActionNotValidException or EmailOrPasswordNotValidException) code = HttpStatusCode.BadRequest;
            else if (exception is UserNotAuthorizedException) code = HttpStatusCode.Forbidden;
            else if (exception is UserNotAuthenticatedException) code = HttpStatusCode.Unauthorized;

            var error = new ResponseProblemDto
            {
                Status = code.GetHashCode(),
                Type = exception.GetType().Name,
                Title = exception.Message,
                Error = exception.Message,
                TraceId = context.TraceIdentifier
            };

            if (exception.IsSystemException())
            {
                if (StaticValue.envType is EnvType.Development)
                {
                    error.Error = exception.StackTrace;
                }
                else
                {
                    error.Title = "Something wrong happened";
                    error.Error = "Please contact administrator for help.";
                }
            }
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsJsonAsync(error);
        }
    }
}
