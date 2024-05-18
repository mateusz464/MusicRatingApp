using System.Net;
using FluentValidation;

namespace MusicRatingApp.Api.Endpoints;

public class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var endpoint = context.HttpContext.GetEndpoint();
        var argToValidate = context.Arguments.OfType<T>().FirstOrDefault() ?? throw new Exception(
            $"You have added a validation filter of type {typeof(T)}, but no argument of that type was found. Endpoint: {endpoint?.DisplayName}");
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

        if (validator is null)
        {
            return await next.Invoke(context);
        }

        var validationResult = await validator.ValidateAsync(argToValidate);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary(),
                statusCode: (int)HttpStatusCode.UnprocessableEntity);
        }

        return await next.Invoke(context);
    }
}