using Api.Common.Extensions;
using Api.Common.Filters;
using Application.Features.Registrations.CreateRegistration;

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
            CreateRegistrationHandler handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(command, cancellationToken);

            if (!result.IsSuccess)
            {
                return result.ToProblemDetails();
            }

            return Results.Created(
                $"/api/registrations/{result.Value!.Id}",
                result.Value);
        })
        .AddEndpointFilter<ValidationFilter<CreateRegistrationCommand>>()
        .WithName("CreateRegistration")
        .WithSummary("Create a new car show registration");

        return app;
    }
}
