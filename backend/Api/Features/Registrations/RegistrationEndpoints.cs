using Api.Common.Extensions;
using Application.Features.Registrations.CreateRegistration;
using FluentValidation;

namespace Api.Features.Registrations;

public static class RegistrationEndpoints
{
    public static IEndpointRouteBuilder MapRegistrationEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/registrations")
            .WithTags("Registrations");

        group.MapPost("/", async (
            CreateRegistrationCommand command,
            IValidator<CreateRegistrationCommand> validator,
            CreateRegistrationHandler handler,
            CancellationToken cancellationToken) =>
        {
            var validationResult = await validator.ValidateAsync(
                command,
                cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        x => x.Key,
                        x => x.Select(error => error.ErrorMessage).ToArray());

                return Results.ValidationProblem(errors);
            }

            var result = await handler.Handle(command, cancellationToken);

            if (!result.IsSuccess)
            {
                return result.ToProblemDetails();
            }

            return Results.Created(
                $"/api/registrations/{result.Value!.Id}",
                result.Value);
        })
        .WithName("CreateRegistration")
        .WithSummary("Create a new car show registration");

        return app;
    }
}

