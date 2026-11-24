using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Registrations.GetRegistrationSummary;

public sealed record GetRegistrationSummaryResponse(
    int TotalRegistrations,
    int Confirmed,
    int CheckedIn,
    int Pending,
    int Cancelled,
    decimal TotalFees,
    decimal TotalPayments,
    decimal UncollectedPayments);
