using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Registrations.GetRegistrationById;

public sealed class GetRegistrationByIdHandler
    : IQueryHandler<GetRegistrationByIdQuery, Result<GetRegistrationByIdResponse>>
{
    private readonly IAppDbContext _context;

    public GetRegistrationByIdHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetRegistrationByIdResponse>> HandleAsync(
        GetRegistrationByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var registration = await _context.Registrations
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == query.Id,
                cancellationToken);

        if (registration is null)
        {
            return Result<GetRegistrationByIdResponse>.Failure(
                Error.NotFound(
                    "Registration.NotFound",
                    $"Registration with ID '{query.Id}' was not found."));
        }

        var response = new GetRegistrationByIdResponse(registration);

        return Result<GetRegistrationByIdResponse>.Success(response);
    }
}
