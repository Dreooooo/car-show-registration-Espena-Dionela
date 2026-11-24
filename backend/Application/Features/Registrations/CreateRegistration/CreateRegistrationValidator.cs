using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Application.Features.Registrations;

namespace Application.Features.Registrations.CreateRegistration;

public class CreateRegistrationValidator : AbstractValidator<CreateRegistrationCommand>
{
    public CreateRegistrationValidator()
    {
        RuleFor(x => x.ParticipantName)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.ContactNumber)
            .NotEmpty()
            .Matches(@"^09\d{9}$")
            .WithMessage("Contact number must be a valid 11-digit Philippine mobile number.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(x => x.CarMakeModel)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.PlateNumber)
            .NotEmpty()
            .MaximumLength(15);

        RuleFor(x => x.RegistrationType)
            .IsInEnum();

        RuleFor(x => x.AmountPaid)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Amount paid cannot be negative.");

        RuleFor(x => x.AmountPaid)
            .Must((command, amountPaid) =>
                amountPaid <= RegistrationFees.GetFee(command.RegistrationType))
            .When(x => Enum.IsDefined(x.RegistrationType))
            .WithMessage("Amount paid cannot exceed the registration fee.");
    }
}

