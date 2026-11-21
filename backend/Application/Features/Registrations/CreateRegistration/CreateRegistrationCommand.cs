using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Features.Registrations.CreateRegistration;

public sealed record CreateRegistrationCommand(
    string ParticipantName,
    string ContactNumber,
    string Email,
    string Address,
    string CarMakeModel,
    string PlateNumber,
    RegistrationType RegistrationType,
    decimal AmountPaid
);
