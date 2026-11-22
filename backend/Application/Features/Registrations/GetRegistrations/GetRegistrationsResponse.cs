using Domain.Entities;

namespace Application.Features.Registrations.GetRegistrations;

public sealed record GetRegistrationsResponse(
    IReadOnlyList<Registration> Registrations,
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages);