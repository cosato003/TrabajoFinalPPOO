using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HotelManagementApp.Pages.Reservas
{
    // This answers /Reservas/GetRoomsByType?handler=GetRoomsByType
    // Returns JSON
    //REminder write GetRoomsByType.cshtml
    public class Actualizar : PageModel
    {
        private readonly HotelManagementService _hotelManagementService;

        public Actualizar(HotelManagementService hotelManagementService)
        {
            _hotelManagementService = hotelManagementService;
        }

        public async Task<IActionResult> OnGetAsync(string IdNumber, float Discount)
        {
            var cliente = await _hotelManagementService.GetCustomerAsync(IdNumber);
            cliente.Discount = Discount;
            await _hotelManagementService.UpdateCustomerAsync(cliente);
            return new OkResult();
        }
    }
}
