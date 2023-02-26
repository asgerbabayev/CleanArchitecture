using Microsoft.AspNetCore.Mvc.Filters;
using NotFoundException   = CleanArchitecture.Application.Common.Exceptions.NotFoundException;
using ValidationException = CleanArchitecture.Application.Common.Exceptions.ValidationException;

namespace CleanArchitecture.WebUI.Filters;
public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ValidationException) ,HandleValidationException },
            { typeof(NotFoundException) ,HandleNotFoundException }
        };
    }

    // Attribute kullandığınız alan içerisinde bi exception alırsanız, çalışacak olan metot
    public override void OnException(ExceptionContext context)
    {
        this.HandleException(context);
        base.OnException(context);
    }


    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }


        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }


    }


    private void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;

        var details = new ValidationProblemDetails(exception.Errors)
        {
            Type = "https://code.edu.az/"
        };

        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;

    }
    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = (NotFoundException)context.Exception;
        var details = new ProblemDetails()
        {
            Type = "https://code.edu.az/",
            Title = "Code Edu Not Found Excetion",
            Detail = $"Merak ediyosan, linke tikla bana neden soruyon? {exception.Message}"
        };
        context.Result = new NotFoundObjectResult(details);
        context.ExceptionHandled = true;
    }

    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState)
        {

            Type = "https://code.edu.az/",
            Title = "Yine ne halt yedin?",
            Detail = "Senin gibi kullanici olmaz olsun"

        };
        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }
}
