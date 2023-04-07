using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException badRequestException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (ForbiddenMoveException forbiddenMoveException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(forbiddenMoveException.Message);
            }
            catch (Exceptions.UnauthorizedAccessException unauthorizedAccessException)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(unauthorizedAccessException.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
