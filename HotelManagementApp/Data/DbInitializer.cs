using System;
using System.Linq;
using EjercicioFinalOOP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class DbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new HotelContext(serviceProvider.GetRequiredService<DbContextOptions<HotelContext>>()))
        {
            // Look for any Rooms.
            if (context.Rooms.Any())
            {
                return;   // DB has been seeded
            }

            // Crear habitaciones sencillas
            for (int floor = 2; floor <= 4; floor++)
            {
                for (int i = 1; i <= 10; i++)
                {
                    context.SimpleRooms.Add(new SimpleRoom
                    {
                        RoomNumber = (uint)((floor - 2) * 10 + i),
                        Floor = (uint)floor,
                        NightlyRate = 200000,
                        BedType = (i % 2 == 0) ? BedType.Double : BedType.Twin
                    });
                }
            }

            // Crear habitaciones ejecutivas
            for (int i = 1; i <= 10; i++)
            {
                context.ExecutiveRooms.Add(new ExecutiveRoom
                {
                    RoomNumber = (uint)(30 + i),
                    Floor = 5,
                    NightlyRate = 350000,
                    BedType = (i % 2 == 0) ? BedType.Queen : BedType.SemiDouble
                });
            }

            // Crear suites
            for (int i = 1; i <= 5; i++)
            {
                context.Suites.Add(new Suite
                {
                    RoomNumber = (uint)(40 + i),
                    Floor = 6,
                    NightlyRate = 500000,
                    BedType = (i % 2 == 0) ? BedType.King : BedType.Queen
                });
            }

            context.SaveChanges();

            // Crear huéspedes de ejemplo
            for (int i = 1; i <= 5; i++)
            {
                context.Guests.Add(new Guest
                {
                    Name = $"Huésped {i}",
                    IdType = "CC",
                    IdNumber = $"ID{i:000}",
                    Phone = $"123-456-789{i}"
                });
            }

            context.SaveChanges();

            // Crear clientes de ejemplo
            var customers = new Customer[]
            {
                new Customer { Name = "Alice Johnson", IdType = "CC", IdNumber = "789012", Phone = "777-888-999", LoyaltyCode = "LOYAL123", Discount = 10.0f },
                new Customer { Name = "Bob Brown", IdType = "CC", IdNumber = "210987", Phone = "000-111-222", LoyaltyCode = "LOYAL456", Discount = 5.0f }
            };

            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
        }
    }
}
