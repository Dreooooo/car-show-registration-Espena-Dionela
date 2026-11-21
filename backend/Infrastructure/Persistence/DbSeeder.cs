using System;
using System.Collections.Generic;
using System.Text;
using Application.Registrations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(
        AppDbContext context,
        CancellationToken cancellationToken = default)
    {
        // Do not insert duplicate sample data.
        if (await context.Registrations.AnyAsync(cancellationToken))
        {
            return;
        }

        var samples = new[]
        {
            ("Juan Dela Cruz", "09170000001", "juan@example.com", "Biñan, Laguna", "Toyota Vios", "ABC-1001", RegistrationType.Exhibitor, RegistrationStatus.Confirmed, 1500m),
            ("Maria Santos", "09170000002", "maria@example.com", "Santa Rosa, Laguna", "Honda Civic", "ABC-1002", RegistrationType.Competitor, RegistrationStatus.Pending, 1000m),
            ("Pedro Reyes", "09170000003", "pedro@example.com", "Makati City", "Mitsubishi Lancer", "ABC-1003", RegistrationType.VIP, RegistrationStatus.Confirmed, 5000m),
            ("Ana Garcia", "09170000004", "ana@example.com", "Quezon City", "Nissan GT-R", "ABC-1004", RegistrationType.Competitor, RegistrationStatus.CheckedIn, 2500m),
            ("Carlos Mendoza", "09170000005", "carlos@example.com", "Pasig City", "Ford Mustang", "ABC-1005", RegistrationType.Exhibitor, RegistrationStatus.Pending, 0m),
            ("Sofia Ramos", "09170000006", "sofia@example.com", "Cabuyao, Laguna", "Honda City", "ABC-1006", RegistrationType.VIP, RegistrationStatus.Confirmed, 3000m),
            ("Miguel Torres", "09170000007", "miguel@example.com", "Taguig City", "Toyota Supra", "ABC-1007", RegistrationType.Competitor, RegistrationStatus.Confirmed, 2500m),
            ("Isabella Flores", "09170000008", "isabella@example.com", "Calamba, Laguna", "Mazda MX-5", "ABC-1008", RegistrationType.Exhibitor, RegistrationStatus.CheckedIn, 1500m),
            ("Gabriel Cruz", "09170000009", "gabriel@example.com", "Manila City", "Subaru WRX", "ABC-1009", RegistrationType.VIP, RegistrationStatus.Pending, 2000m),
            ("Andrea Lopez", "09170000010", "andrea@example.com", "Muntinlupa City", "Hyundai Elantra", "ABC-1010", RegistrationType.Competitor, RegistrationStatus.Cancelled, 0m),
            ("Daniel Castillo", "09170000011", "daniel@example.com", "Biñan, Laguna", "BMW M3", "ABC-1011", RegistrationType.Exhibitor, RegistrationStatus.Confirmed, 1500m),
            ("Patricia Aquino", "09170000012", "patricia@example.com", "Cavite City", "Mercedes-Benz C-Class", "ABC-1012", RegistrationType.VIP, RegistrationStatus.CheckedIn, 5000m),
            ("Nathan Villanueva", "09170000013", "nathan@example.com", "Bacoor, Cavite", "Mitsubishi Montero", "ABC-1013", RegistrationType.Competitor, RegistrationStatus.Pending, 0m),
            ("Camille Bautista", "09170000014", "camille@example.com", "Tagaytay City", "Toyota 86", "ABC-1014", RegistrationType.Exhibitor, RegistrationStatus.Confirmed, 750m),
            ("Joshua Fernandez", "09170000015", "joshua@example.com", "Mandaluyong City", "Nissan Silvia", "ABC-1015", RegistrationType.VIP, RegistrationStatus.Confirmed, 5000m),
            ("Nicole Rivera", "09170000016", "nicole@example.com", "General Trias, Cavite", "Honda Accord", "ABC-1016", RegistrationType.Competitor, RegistrationStatus.CheckedIn, 2500m),
            ("Adrian Morales", "09170000017", "adrian@example.com", "Pasay City", "Chevrolet Camaro", "ABC-1017", RegistrationType.Exhibitor, RegistrationStatus.Pending, 0m),
            ("Bianca Gutierrez", "09170000018", "bianca@example.com", "Los Baños, Laguna", "Volkswagen Golf", "ABC-1018", RegistrationType.VIP, RegistrationStatus.Confirmed, 2500m),
            ("Ethan Ramirez", "09170000019", "ethan@example.com", "Imus, Cavite", "Ford Ranger", "ABC-1019", RegistrationType.Competitor, RegistrationStatus.Pending, 1500m),
            ("Chloe Mendoza", "09170000020", "chloe@example.com", "San Pedro, Laguna", "Toyota Fortuner", "ABC-1020", RegistrationType.Exhibitor, RegistrationStatus.CheckedIn, 1500m),
            ("Liam Gonzales", "09170000021", "liam@example.com", "Quezon City", "Porsche 911", "ABC-1021", RegistrationType.VIP, RegistrationStatus.Confirmed, 5000m),
            ("Mia Hernandez", "09170000022", "mia@example.com", "Santa Rosa, Laguna", "Suzuki Swift", "ABC-1022", RegistrationType.Competitor, RegistrationStatus.Cancelled, 0m),
            ("Noah Torres", "09170000023", "noah@example.com", "Makati City", "BMW 320i", "ABC-1023", RegistrationType.Exhibitor, RegistrationStatus.Pending, 500m),
            ("Ella Martinez", "09170000024", "ella@example.com", "Carmona, Cavite", "Honda CR-V", "ABC-1024", RegistrationType.VIP, RegistrationStatus.CheckedIn, 5000m),
            ("Lucas Garcia", "09170000025", "lucas@example.com", "Biñan, Laguna", "Toyota GR Yaris", "ABC-1025", RegistrationType.Competitor, RegistrationStatus.Confirmed, 2500m)
        };

        var registrations = samples.Select((sample, index) => new Registration
        {
            RegistrationNumber = $"CS27-{index + 1:D4}",
            RegistrationDate = DateTime.UtcNow.AddDays(-(index + 1)),
            ParticipantName = sample.Item1,
            ContactNumber = sample.Item2,
            Email = sample.Item3,
            Address = sample.Item4,
            CarMakeModel = sample.Item5,
            PlateNumber = sample.Item6,
            RegistrationType = sample.Item7,
            Status = sample.Item8,
            RegistrationFee = RegistrationFees.GetFee(sample.Item7),
            AmountPaid = sample.Item9
        }).ToList();

        context.Registrations.AddRange(registrations);

        await context.SaveChangesAsync(cancellationToken);
    }
}

