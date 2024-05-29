using EjercicioFinalOOP.Models;
using EjercicioFinalOOP.Systems;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
public class HotelManagementService
{
    private readonly HotelContext _context;
    private readonly HotelManagementSystem _hotelManagementSystem;

    public HotelManagementService(HotelContext context)
    {
        _context = context;
        _hotelManagementSystem = new HotelManagementSystem(new RestaurantSystem(), new BillingSystem());
        LoadDataFromDatabase();
    }

    private void LoadDataFromDatabase()
    {
        _hotelManagementSystem.Rooms = _context.Rooms.ToList();
        _hotelManagementSystem.Guests = _context.Guests.ToList();
        _hotelManagementSystem.Customers = _context.Customers.ToList();
        _hotelManagementSystem.Reservations = _context.Reservations.ToList();
        _hotelManagementSystem.Invoices = _context.Invoices.ToList();
    }

    private async Task SyncDatabaseAsync()
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        foreach (var invoice in _hotelManagementSystem.Invoices)
        {
            var existingInvoice = await _context.Invoices.FindAsync(invoice.Id);
            if (existingInvoice == null)
            {
                _context.Invoices.Add(invoice);
            }
            else
            {
                _context.Entry(existingInvoice).CurrentValues.SetValues(invoice);
            }
        }
        foreach (var room in _hotelManagementSystem.Rooms)
        {
            var existingRoom = await _context.Rooms.FindAsync(room.RoomNumber);
            if (existingRoom == null)
            {
                _context.Rooms.Add(room);
            }
            else
            {
                _context.Entry(existingRoom).CurrentValues.SetValues(room);
            }
        }

        foreach (var guest in _hotelManagementSystem.Guests)
        {
            var existingGuest = await _context.Guests.FindAsync(guest.IdNumber);
            if (existingGuest == null)
            {
                _context.Guests.Add(guest);
            }
            else
            {
                _context.Entry(existingGuest).CurrentValues.SetValues(guest);
            }
        }

        foreach (var customer in _hotelManagementSystem.Customers)
        {
            var existingCustomer = await _context.Customers.FindAsync(customer.IdNumber);
            if (existingCustomer == null)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
            }
        }

        foreach (var reservation in _hotelManagementSystem.Reservations)
        {
            var existingReservation = await _context.Reservations.FindAsync(reservation.ReservationNumber);
            if (existingReservation == null)
            {
                _context.Reservations.Add(reservation);
            }
            else
            {
                _context.Entry(existingReservation).CurrentValues.SetValues(reservation);
            }
        }

        
        await _context.SaveChangesAsync();
                
        await transaction.CommitAsync();
    }

    public async Task<List<Reservation>> GetReservationsForRoomAsync(uint roomNumber)
    {
        return await _context.Reservations
            .Include(r => r.ReservedGuest)
            .Where(r => r.ReservedRoom.RoomNumber == roomNumber && r.IsActive)
            .ToListAsync();
    }

    public async Task CheckInAsyncFromReservation(uint reservationId)
    {
        var reservation = await _context.Reservations
            .Include(r => r.ReservedGuest)
            .Include(r => r.ReservedRoom)
            .FirstOrDefaultAsync(r => r.ReservationNumber == reservationId);

        if (reservation == null)
        {
            throw new Exception("Reserva no encontrada.");
        }

        _hotelManagementSystem.CheckIn(reservation.ReservedGuest, reservation.ReservedRoom.RoomNumber, reservation.CheckOutDate);
        reservation.IsActive = false;

        await SyncDatabaseAsync();
    }

    public async Task<List<SimpleRoom>> GetSimpleRoomsAsync()
    {
        return await _context.SimpleRooms.ToListAsync();
    }

    public async Task<List<ExecutiveRoom>> GetExecutiveRoomsAsync()
    {
        return await _context.ExecutiveRooms.ToListAsync();
    }

    public async Task<List<Suite>> GetSuitesAsync()
    {
        return await _context.Suites.ToListAsync();
    }

    public async Task<Room> GetRoomAsync(uint roomNumber)
    {
        // IF is a suite or executive room include minibar and laundry service
        var room = await _context.Rooms.Include(x => x.Invoice)
            .Include(x => x.Invoice.Items)
            .Where(x => x.RoomNumber == roomNumber).FirstAsync();
        if (room is Suite x)
        {
            return await _context.Suites.Include(x => x.Invoice)
                .Include(x => x.Invoice.Items)
                .Include(x => x.Minibar)
                .Where(x => x.RoomNumber == roomNumber).FirstAsync();
        }
        else if (room is ExecutiveRoom y)
        {
            return await _context.ExecutiveRooms.Include(x => x.Invoice)
                .Include(x => x.Invoice.Items)
                .Include(x => x.Minibar)
                .Where(x => x.RoomNumber == roomNumber).FirstAsync();
        }
        return room;
    }

    public async Task<List<Guest>> GetGuestsAsync()
    {
        return await _context.Guests.ToListAsync();
    }

    public async Task<Guest> GetGuestAsync(string idNumber)
    {
        return await _context.Guests.FindAsync(idNumber);
    }

    public async Task<List<Customer>> GetCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }
    public async Task ReduceMinibarItemAsync(uint roomNumber, MinibarItem description, int quantity)
    {
        var room = await _context.Rooms.FindAsync(roomNumber);

        if (room == null)
        {
            throw new Exception("Habitación no encontrada.");
        }

        if(room is Suite suite)
        {
            switch(description ) {
                case MinibarItem.WineBottle:
                    suite.Minibar.WineBottles -= quantity;
                    break;
                case MinibarItem.LiquorBottle:
                    suite.Minibar.LiquorBottles -= quantity;
                    break;
                case MinibarItem.PersonalCareKit:
                    suite.Minibar.PersonalCareKits -= quantity;
                    break;
                case MinibarItem.WaterBottle:
                    suite.Minibar.WaterBottles -= quantity;
                    break;
                case MinibarItem.Soda:
                    suite.Minibar.Sodas -= quantity;
                    break;
                    default:
                    break;
            }

        }
        else if(room is ExecutiveRoom executive)
        {
            switch(description ) {
                case MinibarItem.WineBottle:
                    executive.Minibar.WineBottles -= quantity;
                    break;
                case MinibarItem.LiquorBottle:
                    executive.Minibar.LiquorBottles -= quantity;
                    break;
                case MinibarItem.PersonalCareKit:
                    executive.Minibar.PersonalCareKits -= quantity;
                    break;
                case MinibarItem.WaterBottle:
                    executive.Minibar.WaterBottles -= quantity;
                    break;
                case MinibarItem.Soda:
                    executive.Minibar.Sodas -= quantity;
                    break;
                    default:
                    break;
            }
        }
        else
        {
            throw new Exception("Habitación no es una suite o habitación ejecutiva.");
        }
        await SyncDatabaseAsync();
    }

    public async Task<Customer> GetCustomerAsync(string idNumber)
    {
        return await _context.Customers.FindAsync(idNumber);
    }

    public async Task<List<Reservation>> GetReservationsAsync()
    {
        return await _context.Reservations.ToListAsync();
    }

    public async Task<Reservation> GetReservationAsync(uint reservationNumber)
    {
        return await _context.Reservations.FindAsync(reservationNumber);
    }
    public async Task MakeReservationAsync(uint roomNumber, Guest guest, DateTime checkInDate, DateTime checkOutDate)
    {
        var room = await _context.Rooms.FindAsync(roomNumber);

        if (room == null)
        {
            throw new Exception("Habitación no encontrada.");
        }

        _hotelManagementSystem.MakeReservation(roomNumber, guest, checkInDate, checkOutDate);
        await SyncDatabaseAsync();
    }

    public async Task CancelReservationAsync(uint reservationNumber)
    {
        var reservation = await _context.Reservations.FindAsync(reservationNumber);

        if (reservation == null)
        {
            throw new Exception("Reserva no encontrada.");
        }

        _hotelManagementSystem.CancelReservation(reservationNumber);
        await SyncDatabaseAsync();
    }

    // Métodos para realizar check-in y check-out
    public async Task CheckInAsync(string guestId, uint roomNumber, DateTime outDate)
    {
        var room = await _context.Rooms.FindAsync(roomNumber);

        if (room == null)
        {
            throw new Exception("Habitación no encontrada.");
        }

        // Priorizar reservas
        var reservation = _context.Reservations
            .Include(r => r.ReservedGuest)
            .FirstOrDefault(r => r.ReservedRoom.RoomNumber == roomNumber && r.IsActive && r.CheckInDate <= DateTime.Now && r.CheckOutDate >= DateTime.Now);

        if (reservation != null)
        {
            _hotelManagementSystem.CheckIn(reservation.ReservedGuest, roomNumber, outDate);
            reservation.IsActive = false; // Marcar la reserva como inactiva
            await SyncDatabaseAsync();
            return;
        }

        // Si no hay reserva, buscar entre los clientes
        var customer = await _context.Customers.FindAsync(guestId);
        if (customer != null)
        {
            _hotelManagementSystem.CheckIn(customer, roomNumber, outDate);
            await SyncDatabaseAsync();
            return;
        }

        throw new Exception("Huésped no encontrado. Por favor, asegúrese de que el huésped está registrado como cliente o tiene una reserva activa.");
    }

    public async Task<List<Invoice>> GetInvoicesAsync()
    {
        return await _context.Invoices.Include(i => i.Items).ToListAsync();
    }

    public async Task<Invoice> GetInvoiceAsync(int invoiceId)
    {
        return await _context.Invoices.Include(i => i.Items).FirstOrDefaultAsync(i => i.Id == invoiceId);
    }
    public async Task CheckOutAsync(uint roomNumber)
    {
        var room = await _context.Rooms.FindAsync(roomNumber);
        if (room == null)
        {
            throw new Exception("Habitación no encontrada.");
        }
        LoadDataFromDatabase();
        _hotelManagementSystem.CheckOut(roomNumber);
        await SyncDatabaseAsync();
    }

    // Métodos para gestionar pedidos de restaurante
    public async Task AddOrderAsync(string item, uint roomNumber, bool roomService = false)
    {
        var room = await _context.Rooms.FindAsync(roomNumber);

        if (room == null)
        {
            throw new Exception("Habitación no encontrada.");
        }

        _hotelManagementSystem.AddOrder(item, roomNumber, roomService);
        await SyncDatabaseAsync();
    }

    // Métodos para gestionar servicios de lavandería
    public async Task AddLaundryServiceAsync(uint roomNumber, int quantity, bool ironing)
    {
        var room = await _context.Rooms.FindAsync(roomNumber);

        if (room == null)
        {
            throw new Exception("Habitación no encontrada.");
        }

        _hotelManagementSystem.AddLaundryService(roomNumber, quantity, ironing);
        await SyncDatabaseAsync();
    }

    // Métodos para gestionar huéspedes y clientes
    public async Task AddGuestAsync(Guest guest)
    {
        _context.Guests.Add(guest);
        _hotelManagementSystem.Guests.Add(guest);
        await SyncDatabaseAsync();
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        _hotelManagementSystem.Customers.Add(customer);
        await SyncDatabaseAsync();
    }

    public async Task UpdateGuestAsync(Guest guest)
    {
        var existingGuest = await _context.Guests.FindAsync(guest.IdNumber);
        if (existingGuest == null)
        {
            throw new Exception("Huésped no encontrado.");
        }

        existingGuest.Name = guest.Name;
        existingGuest.IdType = guest.IdType;
        existingGuest.Phone = guest.Phone;
        await SyncDatabaseAsync();
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        var existingCustomer = await _context.Customers.FindAsync(customer.IdNumber);
        if (existingCustomer == null)
        {
            throw new Exception("Cliente no encontrado.");
        }

        existingCustomer.Name = customer.Name;
        existingCustomer.IdType = customer.IdType;
        existingCustomer.Phone = customer.Phone;
        existingCustomer.LoyaltyCode = customer.LoyaltyCode;
        existingCustomer.Discount = customer.Discount;
        await SyncDatabaseAsync();
    }


    public async Task AddMinibarConsumptionAsync(uint roomNumber, string itemDescription, int quantity, float price)
    {
        var room = await _context.Rooms.FindAsync(roomNumber);

        if (room == null)
        {
            throw new Exception("Habitación no encontrada.");
        }

        var invoiceItem = new InvoiceItem
        {
            Type = TypeOfInvoiceItem.AdditionalItem,
            Description = itemDescription,
            UnitCost = price,
            Quantity = quantity,
            Guest = room.Guest
        };

        var invoice = new Invoice
        {
            Items = new List<InvoiceItem> { invoiceItem },
            TotalAmmount = invoiceItem.TotalCost
        };

        room.AddTransaction(invoiceItem);

        await SyncDatabaseAsync();
    }

    public async Task<List<SelectListItem>> GetRoomsByTypeAsync(string type, bool getOccupied = false)
    {
        List<Room> rooms = type switch
        {
            "Simple" => await _context.SimpleRooms.Where(x => getOccupied ? true : x.Occupied == false).ToListAsync<Room>(),
            "Executive" => await _context.ExecutiveRooms.Where(x => getOccupied ? true : x.Occupied == false).ToListAsync<Room>(),
            "Suite" => await _context.Suites.Where(x => getOccupied ? true : x.Occupied == false).ToListAsync<Room>(),
            _ => new List<Room>()
        };

        return rooms.Select(r => new SelectListItem { Value = r.RoomNumber.ToString(), Text = $"Habitación {r.RoomNumber} - Piso {r.Floor}" }).ToList();
    }

}
