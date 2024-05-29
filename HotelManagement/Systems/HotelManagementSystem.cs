using EjercicioFinalOOP.Excepciones;
using EjercicioFinalOOP.Models;

namespace EjercicioFinalOOP.Systems
{
    public class HotelManagementSystem
    {
        public IList<Room> Rooms { get; set; } = new List<Room>();
        public IList<Guest> Guests { get; set; } = new List<Guest>();
        public IList<Customer> Customers { get; set; } = new List<Customer>();
        public IList<Reservation> Reservations { get; set; } = new List<Reservation>();

        public RestaurantSystem RestaurantSystem { get; set; }
        public BillingSystem BillingSystem { get; set; }
        
        public IList<Invoice> Invoices { get; set; }

        public HotelManagementSystem(RestaurantSystem restaurantSystem, BillingSystem billingSystem)
        {
            RestaurantSystem = restaurantSystem;
            BillingSystem = billingSystem;
        }

        public void CheckIn(Person guest, uint roomNumber, DateTime outDate)
        {
            if (guest is Guest guest1)
            {
                // Solo reservas
                var reservation = Reservations.FirstOrDefault(r => r.ReservedRoom.RoomNumber == roomNumber && r.ReservedGuest == guest1 && r.IsActive && r.CheckInDate <= DateTime.Now && r.CheckOutDate >= outDate);
                if (reservation != null)
                {
                    reservation.IsActive = false;
                }
                else
                {
                    throw new Exception("No se encontró la reservación.");
                }
            }

            var room = Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber) ?? throw new RoomNotFoundException((int)roomNumber);
            if (room.Occupied && room.OutDate <= DateTime.Now)
            {
                throw new Exception("Cuarto ocupado.");
            }
            else if (room.Occupied && room.OutDate > DateTime.Now)
            {
                throw new Exception("Cuarto ocupado. No se puede hacer check-in hasta que el huésped actual se retire. Hacer un check-out primero.");
            }

            if (Reservations.Any(r => r.ReservedRoom.RoomNumber == roomNumber && r.IsActive && r.CheckInDate <= outDate && r.CheckOutDate >= outDate))
            {
                throw new Exception("Cuarto reservado.");
            }

            var invoice = new Invoice()
            {
                Id = Invoices.Count + 1,
            };
            if (guest is Customer customer)
            {
                invoice.Discount = customer.Discount;
            }
            Invoices.Add(invoice);
            room.CheckIn(guest, outDate, invoice);
        }

        public void CheckOut(uint roomNumber)
        {
            var room = Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber) ?? throw new RoomNotFoundException((int)roomNumber);
            room.CheckOut();
            BillingSystem.AppendInvoices(room.GetTransactions());
        }

        public void AddRoom(Room room)
        {
            Rooms.Add(room);
        }

        public void AddOrder(string item, uint roomNumber, bool roomService = false)
        {
            var room = Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber) ?? throw new RoomNotFoundException((int)roomNumber);
            RestaurantSystem.AddOrder(item, room);
            if (roomService)
            {
                RestaurantSystem.CalculateServiceFees(room);
            }
        }

        public void AddLaundryService(uint roomNumber, int quantity, bool ironing)
        {
            var room = Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber) ?? throw new RoomNotFoundException((int)roomNumber);
            const float LaundryServiceFee = 12000;
            const float IroningFee = 9000;
            var totalCost = LaundryServiceFee * quantity + (ironing ? IroningFee : 0);
            room.AddTransaction(new InvoiceItem(TypeOfInvoiceItem.AdditionalItem, "Servicio de lavandería", totalCost, quantity)
            {
                Guest = room.Guest ?? throw new Exception("No hay un huésped asignado a la habitación.")
            });
        }

        public void CancelReservation(uint reservationNumber)
        {
            var reservation = Reservations.FirstOrDefault(r => r.ReservationNumber == reservationNumber) ?? throw new Exception($"No se encontró la reservación {reservationNumber}");
            reservation.CancelReservation();
        }

        public void MakeReservation(uint roomNumber, Guest guest, DateTime checkInDate, DateTime checkOutDate)
        {
            var room = Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber) ?? throw new Exception($"Room {roomNumber} not found");

            if (room.Occupied && room.OutDate <= checkInDate)
            {
                throw new Exception("Cuarto ocupado.");
            }

            if (Reservations.Any(r => r.ReservedRoom.RoomNumber == roomNumber && r.IsActive && r.CheckInDate <= checkOutDate && r.CheckOutDate >= checkInDate))
            {
                throw new Exception("Cuarto reservado.");
            }

            var reservation = new Reservation((uint)(Reservations.Count + 1), room, guest, checkInDate, checkOutDate);
            Reservations.Add(reservation);
        }

        // Manejo de usuarios
        public void AddGuest(Guest guest)
        {
            Guests.Add(guest);
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public void AddCustomer(string name, string idType, string identification, string phoneNumber)
        {
            var customer = new Customer(name, idType, identification, phoneNumber, "", 0.0f);
            Customers.Add(customer);
        }

        public void AddGuest(string name, string idType, string identification, string phoneNumber)
        {
            var guest = new Guest(name, idType, identification, phoneNumber);
            Guests.Add(guest);
        }

        public void UpdateGuest(Guest guest)
        {
            var index = Guests.ToList().FindIndex(g => g.IdNumber == guest.IdNumber);
            if (index == -1)
            {
                throw new Exception("No se encontró el huésped.");
            }
            Guests[index] = guest;
        }

        public void UpdateCustomer(Customer customer)
        {
            var index = Customers.ToList().FindIndex(c => c.IdNumber == customer.IdNumber);
            if (index == -1)
            {
                throw new Exception("No se encontró el cliente.");
            }
            Customers[index] = customer;
        }
    }
}
