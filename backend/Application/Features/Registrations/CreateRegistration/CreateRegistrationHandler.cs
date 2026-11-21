using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Abstractions.Data;
using Application.Registrations;
using Domain.Common;
using Domain.Entities;

namespace Application.Features.Registrations.CreateRegistration;

public sealed class CreateRegistrationHandler
{
    private readonly IAppDbContext _context;

    public CreateRegistrationHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CreateRegistrationResponse>> Handle(
        CreateRegistrationCommand command,
        CancellationToken cancellationToken = default)
    {
        var registration = new Registration
        {
            RegistrationNumber = await GenerateRegistrationNumber(cancellationToken),
            RegistrationDate = DateTime.UtcNow,
            ParticipantName = command.ParticipantName,
            ContactNumber = command.ContactNumber,
            Email = command.Email,
            Address = command.Address,
            CarMakeModel = command.CarMakeModel,
            PlateNumber = command.PlateNumber,
            RegistrationType = command.RegistrationType,
            Status = RegistrationStatus.Pending,
            RegistrationFee = RegistrationFees.GetFee(command.RegistrationType),
            AmountPaid = command.AmountPaid
        };

        _context.Registrations.Add(registration);

        await _context.SaveChangesAsync(cancellationToken);

        var response = new CreateRegistrationResponse(
            registration.Id,
            registration.RegistrationNumber,
            registration.RegistrationDate,
            registration.ParticipantName,
            registration.ContactNumber,
            registration.Email,
            registration.Address,
            registration.CarMakeModel,
            registration.PlateNumber,
            registration.RegistrationType,
            registration.Status,
            registration.RegistrationFee,
            registration.AmountPaid,
            registration.PaymentStatus
        );

        return Result<CreateRegistrationResponse>.Success(response);
    }

    private async Task<string> GenerateRegistrationNumber(
        CancellationToken cancellationToken)
    {
        var count = await _context.Registrations.CountAsync(cancellationToken);

        return $"CS27-{count + 1:D4}";
    }
}

