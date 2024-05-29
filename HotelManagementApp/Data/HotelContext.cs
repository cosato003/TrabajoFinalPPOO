using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using EjercicioFinalOOP.Models;
public class HotelContext : DbContext
{
    public HotelContext(DbContextOptions<HotelContext> options) : base(options) { }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Person> People { get; set; }

    public DbSet<SimpleRoom> SimpleRooms { get; set; }
    public DbSet<Suite> Suites { get; set; }
    public DbSet<ExecutiveRoom> ExecutiveRooms { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    public DbSet<Invoice> Invoices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>().ToTable("Room");
        modelBuilder.Entity<Person>().ToTable("Person");
        modelBuilder.Entity<Reservation>().ToTable("Reservation");
        modelBuilder.Entity<Invoice>().ToTable("Invoice");

        modelBuilder.Entity<Room>().HasKey(r => r.RoomNumber);
        modelBuilder.Entity<Person>().HasKey(g => g.IdNumber);
        modelBuilder.Entity<Reservation>().HasKey(r => r.ReservationNumber);
        modelBuilder.Entity<Invoice>().HasKey(i => i.Id);
        
    }
}