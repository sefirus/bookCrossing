using System.Net;
using Core.Exceptions;
using Core.ViewModels.ExceptionViewModels;
using Newtonsoft.Json;

namespace BookCrossingBackEnd.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException e)
        {
            await HandleExceptionAsync(context, HttpStatusCode.NotFound, e.Message);
        }
        catch (BadRequestException e)
        {
            await HandleExceptionAsync(context, HttpStatusCode.BadRequest, e.Message);
        }
        catch (VolumeIncompleteException e)
        {
            var message = JsonConvert.SerializeObject(new VolumeIncompleteExceptionViewModel()
            {
                MissingProperties = e.MissingProperties,
                Volume = e.Volume
            });
            await HandleExceptionAsync(context, HttpStatusCode.TemporaryRedirect, message);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, e.GetType().Name + e.StackTrace);
        }
    }
    
    private Task HandleExceptionAsync(HttpContext context, HttpStatusCode errorCode, string errorMessage)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)errorCode;
        return context.Response.WriteAsync(new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            Message = errorMessage
        }.ToString());
    }

}