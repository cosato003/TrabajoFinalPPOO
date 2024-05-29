using Microsoft.AspNetCore.Mvc.RazorPages;
using EjercicioFinalOOP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagementApp.Pages.Reservas
{
    public class IndexModel : PageModel
    {
        private readonly HotelManagementService _hotelManagementService;

        public IndexModel(HotelManagementService hotelManagementService)
        {
            _hotelManagementService = hotelManagementService;
        }

        public IList<Reservation> Reservas { get; set; }

        public async Task OnGetAsync()
        {
            Reservas = await _hotelManagementService.GetReservationsAsync();
        }
    }
}
