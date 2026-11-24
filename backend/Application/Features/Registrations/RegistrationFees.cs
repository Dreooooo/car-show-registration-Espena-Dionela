using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Features.Registrations;

public static class RegistrationFees
{
    public static decimal GetFee(RegistrationType registrationType)
    {
        return registrationType switch
        {
            RegistrationType.Exhibitor => 1500m,
            RegistrationType.Competitor => 2500m,
            RegistrationType.VIP => 5000m,
            _ => throw new ArgumentOutOfRangeException(
                nameof(registrationType),
                "Invalid registration type."
            )
        };
    }
}

