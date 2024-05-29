// Namespace para las Clases Concretas
namespace EjercicioFinalOOP.Models
{
    public class Reservation
    {
        public uint ReservationNumber { get; set; }
        public Room ReservedRoom { get; set; }
        public Person ReservedGuest { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public bool IsActive { get; set; }

        public Reservation(uint reservationNumber, Room reservedRoom, Person reservedGuest, DateTime checkInDate, DateTime checkOutDate)
        {
            ReservationNumber = reservationNumber;
            ReservedRoom = reservedRoom;
            ReservedGuest = reservedGuest;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            IsActive = true;
        }

        public Reservation() {}

        public void CancelReservation()
        {
            IsActive = false;
        }
    }
}
