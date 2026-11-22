using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Registrations.GetRegistrations;

public sealed class GetRegistrationsHandler
    : IQueryHandler<GetRegistrationsQuery, Result<GetRegistrationsResponse>>
{
    private readonly IAppDbContext _context;

    public GetRegistrationsHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetRegistrationsResponse>> HandleAsync(
        GetRegistrationsQuery query,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Registration> registrations =
            _context.Registrations.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Type))
        {
            if (!Enum.TryParse<RegistrationType>(
                    query.Type, true, out var type) ||
                !Enum.IsDefined(type))
            {
                return Result<GetRegistrationsResponse>.Failure(
                    Error.Validation(
                        "Registration.Validation.InvalidType",
                        "The registration type is invalid."));
            }

            registrations = registrations.Where(
                x => x.RegistrationType == type);
        }

        if (!string.IsNullOrWhiteSpace(query.Status))
        {
            if (!Enum.TryParse<RegistrationStatus>(
                    query.Status, true, out var status) ||
                !Enum.IsDefined(status))
            {
                return Result<GetRegistrationsResponse>.Failure(
                    Error.Validation(
                        "Registration.Validation.InvalidStatus",
                        "The registration status is invalid."));
            }

            registrations = registrations.Where(
                x => x.Status == status);
        }

        if (!string.IsNullOrWhiteSpace(query.ParticipantName))
        {
            var searchTerm = query.ParticipantName.Trim().ToLower();

            registrations = registrations.Where(
                x => x.ParticipantName.ToLower().Contains(searchTerm));
        }

        var totalCount = await registrations.CountAsync(cancellationToken);

        var totalPages = (int)Math.Ceiling(
            totalCount / (double)query.PageSize);

        var items = await registrations
            .OrderByDescending(x => x.RegistrationDate)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        var response = new GetRegistrationsResponse(
            items,
            query.Page,
            query.PageSize,
            totalCount,
            totalPages);

        return Result<GetRegistrationsResponse>.Success(response);
    }
}

