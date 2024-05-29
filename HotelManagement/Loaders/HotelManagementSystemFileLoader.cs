using EjercicioFinalOOP.Interfaces;
using EjercicioFinalOOP.Systems;

namespace EjercicioFinalOOP.Loaders
{
    public class HotelManagementSystemFileLoader(string customerFilePath, string roomFilePath, string invoiceFilePath)
        : IHotelManagementSystemService
    {
        CustomerFileLoader CustomerFileLoader { get; set; } = new(customerFilePath);
        RoomFileLoader RoomFileLoader { get; set; } = new(roomFilePath);
        InvoiceFileLoader InvoiceFileLoader { get; set; } = new(invoiceFilePath);

        public HotelManagementSystem LoadHotelManagementSystem()
        {
            RestaurantSystem restaurantSystem = new RestaurantSystem();
            BillingSystem billingSystem = new BillingSystem();
            HotelManagementSystem hotelManagementSystem = new HotelManagementSystem(restaurantSystem, billingSystem);

            foreach (var room in RoomFileLoader.GetAvailableRooms())
            {
                hotelManagementSystem.AddRoom(room);
            }

            foreach (var invoice in InvoiceFileLoader.GetAllInvoices())
            {
                billingSystem.AppendInvoices(invoice.GetItems());
            }

            return hotelManagementSystem;
        }

        public void SaveHotelManagementSystem(HotelManagementSystem hotelManagementSystem)
        {
            // Guardar en archivos
            hotelManagementSystem.Rooms.ToList().ForEach(r => RoomFileLoader.UpdateRoom(r));
        }
    }

}
