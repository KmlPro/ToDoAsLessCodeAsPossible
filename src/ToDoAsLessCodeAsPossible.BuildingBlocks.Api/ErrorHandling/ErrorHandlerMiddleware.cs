using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.ErrorHandling.Mapper;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.ErrorHandling.Payload;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.ErrorHandling;

internal sealed class ErrorHandlerMiddleware : IMiddleware
{
    private readonly IExceptionToResponseMapper _exceptionToResponseMapper;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger,
        IExceptionToResponseMapper exceptionToResponseMapper,
        IWebHostEnvironment hostingEnvironment)
    {
        _logger = logger;
        _exceptionToResponseMapper = exceptionToResponseMapper;
        _hostingEnvironment = hostingEnvironment;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            await HandleErrorAsync(context, exception);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception exception)
    {
        var title = "Unexpected Error on Server side";
        var errorResponse = _exceptionToResponseMapper.Map(exception);
        
        if (errorResponse is null)
        {
            dynamic payload = CreatePayload(exception, title);
            errorResponse = new ErrorResponse(HttpStatusCode.InternalServerError, payload);
        }

        context.Response.StatusCode = (int)errorResponse.StatusCode;
        await context.Response.WriteAsJsonAsync(errorResponse.Response);
    }

    private dynamic CreatePayload(Exception exception, string title)
    {
        return _hostingEnvironment.IsDevelopment()
            ? new DebugErrorPayload(title, exception.Message, exception.InnerException?.ToString(), exception.StackTrace)
            : new ErrorPayload(title, new List<string>());
    }
}