using Microsoft.AspNetCore.Mvc.RazorPages;
using EjercicioFinalOOP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagementApp.Pages.Habitaciones
{
    public class IndexModel : PageModel
    {
        private readonly HotelManagementService _hotelManagementService;

        public IndexModel(HotelManagementService hotelManagementService)
        {
            _hotelManagementService = hotelManagementService;
        }

        public IList<SimpleRoom> SimpleRooms { get; set; }
        public IList<ExecutiveRoom> ExecutiveRooms { get; set; }
        public IList<Suite> Suites { get; set; }

        public async Task OnGetAsync()
        {
            SimpleRooms = await _hotelManagementService.GetSimpleRoomsAsync();
            ExecutiveRooms = await _hotelManagementService.GetExecutiveRoomsAsync();
            Suites = await _hotelManagementService.GetSuitesAsync();
        }
    }
}
