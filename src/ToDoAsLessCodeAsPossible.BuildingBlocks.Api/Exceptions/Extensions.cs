using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Exceptions.Mapper;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Exceptions;

public static class Extensions
{
    /// <summary> Add default exception to response mapper for generic exceptions like:
    /// <list type="bullet">
    /// <item>
    /// <description>InvalidRequestException => (400)BadRequest</description>
    /// </item>
    /// </list>
    /// </summary>
    public static IServiceCollection AddDefaultExceptionToResponseMapper(this IServiceCollection services)
    {
        services.AddSingleton<IExceptionToResponseMapper, DefaultExceptionToResponseMapper>();

        return services;
    }
    
    /// <summary>
    /// Registers Exception to Response Middleware in DI Container
    /// </summary>
    public static IServiceCollection AddExceptionToResponseMapping(this IServiceCollection services)
    {
        services.AddScoped<ErrorHandlerMiddleware>();
        return services;
    }

    /// <summary>
    /// Before use make sure you registered required instances using AddExceptionToResponseMapping() method and one of the following things:
    /// <list type="bullet">
    /// <item>
    /// <description>Added default Exception to Response Mapper (AddDefaultExceptionToResponseMapper)</description>
    /// </item>
    /// <item>
    /// <description>Create own Exception to Response Mapper (class that implements IExceptionToResponseMapper interface) and register in DI container (recommended as Singleton)</description>
    /// </item>
    /// </list>
    /// </summary>
    public static WebApplication UseExceptionToResponseMapping(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
        return app;
    }
}