using Api.Common.Extensions;
using Api.Common.Filters;
using Application.Features.Registrations.CreateRegistration;
using Application.Features.Registrations.GetRegistrations;
using Application.Features.Registrations.GetRegistrationById;
using Application.Features.Registrations.GetRegistrationSummary;

namespace Api.Features.Registrations;

public static class RegistrationEndpoints
{
    public static IEndpointRouteBuilder MapRegistrationEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/registrations")
            .WithTags("Registrations");

        // POST: Create a new registration
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

        // GET: Retrieve registrations with filtering and pagination
        group.MapGet("/", async (
            [AsParameters] GetRegistrationsQuery query,
            GetRegistrationsHandler handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.HandleAsync(
                query, cancellationToken);

            if (!result.IsSuccess)
            {
                return result.ToProblemDetails();
            }

            return Results.Ok(result.Value);
        })
        .AddEndpointFilter<ValidationFilter<GetRegistrationsQuery>>()
        .WithName("GetRegistrations")
        .WithSummary("Get registrations with filtering and pagination");

        // GET: Retrieve a registration by ID
        group.MapGet("/{id:guid}", async (
            Guid id,
            GetRegistrationByIdHandler handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetRegistrationByIdQuery(id);

            var result = await handler.HandleAsync(
                query, cancellationToken);

            if (!result.IsSuccess)
            {
                return result.ToProblemDetails();
            }

            return Results.Ok(result.Value!.Registration);
        })
        .WithName("GetRegistrationById")
        .WithSummary("Get a registration by ID");

        // GET: Retrieve registration summary for dashboard KPI cards
        group.MapGet("/summary", async (
            GetRegistrationSummaryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetRegistrationSummaryQuery();

            var result = await handler.HandleAsync(
                query, cancellationToken);

            if (!result.IsSuccess)
            {
                return result.ToProblemDetails();
            }

            return Results.Ok(result.Value);
        })
        .WithName("GetRegistrationSummary")
        .WithSummary("Get registration summary for dashboard KPI cards");

        return app;
    }
}
