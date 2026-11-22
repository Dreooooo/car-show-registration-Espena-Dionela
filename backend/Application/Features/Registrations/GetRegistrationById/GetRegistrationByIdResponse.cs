using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Features.Registrations.GetRegistrationById;

public sealed record GetRegistrationByIdResponse(
    Registration Registration);

