using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Registrations.GetRegistrationSummary;

public sealed class GetRegistrationSummaryHandler
    : IQueryHandler<GetRegistrationSummaryQuery, Result<GetRegistrationSummaryResponse>>
{
    private readonly IAppDbContext _context;

    public GetRegistrationSummaryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetRegistrationSummaryResponse>> HandleAsync(
        GetRegistrationSummaryQuery query,
        CancellationToken cancellationToken = default)
    {
        var totalRegistrations = await _context.Registrations
            .CountAsync(cancellationToken);

        var confirmed = await _context.Registrations
            .CountAsync(x => x.Status == RegistrationStatus.Confirmed, cancellationToken);

        var checkedIn = await _context.Registrations
            .CountAsync(x => x.Status == RegistrationStatus.CheckedIn, cancellationToken);

        var pending = await _context.Registrations
            .CountAsync(x => x.Status == RegistrationStatus.Pending, cancellationToken);

        var cancelled = await _context.Registrations
            .CountAsync(x => x.Status == RegistrationStatus.Cancelled, cancellationToken);

        var totalFees = await _context.Registrations
            .Where(x => x.Status != RegistrationStatus.Cancelled)
            .SumAsync(x => x.RegistrationFee, cancellationToken);

        var totalPayments = await _context.Registrations
            .Where(x => x.Status != RegistrationStatus.Cancelled)
            .SumAsync(x => x.AmountPaid, cancellationToken);

        var uncollectedPayments = totalFees - totalPayments;

        var response = new GetRegistrationSummaryResponse(
            totalRegistrations,
            confirmed,
            checkedIn,
            pending,
            cancelled,
            totalFees,
            totalPayments,
            uncollectedPayments);

        return Result<GetRegistrationSummaryResponse>.Success(response);
    }
}