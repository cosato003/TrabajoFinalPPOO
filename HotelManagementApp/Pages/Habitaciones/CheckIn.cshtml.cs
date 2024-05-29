using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EjercicioFinalOOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementApp.Pages.Habitaciones
{
    public class CheckInModel : PageModel
    {
        private readonly HotelManagementService _hotelManagementService;

        public CheckInModel(HotelManagementService hotelManagementService)
        {
            _hotelManagementService = hotelManagementService;
        }

        [BindProperty(SupportsGet = true, Name = "id")]
        public uint RoomNumber { get; set; }

        public List<Reservation> Reservations { get; set; }
        public List<Customer> Customers { get; set; }

        [BindProperty]
        public DateTime OutDate { get; set; } = DateTime.Now;

        public async Task OnGetAsync()
        {
            Reservations = await _hotelManagementService.GetReservationsForRoomAsync(RoomNumber);
            Customers = await _hotelManagementService.GetCustomersAsync();
        }

        public async Task<IActionResult> OnPostCheckInReservationAsync(uint reservationId)
        {
            Reservations = await _hotelManagementService.GetReservationsForRoomAsync(RoomNumber);
            Customers = await _hotelManagementService.GetCustomersAsync();

            try
            {

                await _hotelManagementService.CheckInAsyncFromReservation(reservationId);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostCheckInCustomerAsync(string customerId)
        {
            Reservations = await _hotelManagementService.GetReservationsForRoomAsync(RoomNumber);
            Customers = await _hotelManagementService.GetCustomersAsync();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Error en los datos ingresados");
                return Page();
            }
            try
            {
                await _hotelManagementService.CheckInAsync(customerId, RoomNumber, OutDate);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToPage("CheckIn", new { id = RoomNumber });
            }

            return Page();
        }
    }
}
