namespace CleanArchitecture.Application.Common.Behaviours;
public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{

    private ILogger<IRequest> _logger;
    public UnhandledExceptionBehaviour(ILogger<IRequest> logger)
    {
        this._logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogError(ex, "CleanArchitecture Rewuest : Unhandled Exception for Request {Name} {@requestName}", requestName, request);
            throw;
        }
    }
}
