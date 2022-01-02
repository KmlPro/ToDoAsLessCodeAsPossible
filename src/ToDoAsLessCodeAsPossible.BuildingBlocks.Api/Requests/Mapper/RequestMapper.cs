using Microsoft.Extensions.DependencyInjection;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Mapper;

internal class RequestMapper
{
    private readonly IServiceScopeFactory _serviceFactory;

    public RequestMapper(IServiceScopeFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public TUseCase Map<TRequest, TUseCase>(TRequest request, CancellationToken cancellationToken)
    {
        using var scope = _serviceFactory.CreateScope();

        var handler = scope.ServiceProvider.GetService<IRequestToUseCaseMapHandler<TRequest, TUseCase>>();
        
        if (handler == null)
        {
            throw new RequestToUseCaseMapHandlerNotFoundException(typeof(TRequest).Name, typeof(TUseCase).Name);
        }
        
        return handler.Map(request, cancellationToken);
    }
}