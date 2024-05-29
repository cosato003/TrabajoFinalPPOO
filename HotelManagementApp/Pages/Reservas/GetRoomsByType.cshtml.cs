using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagementApp.Pages.Reservas
{
    // This answers /Reservas/GetRoomsByType?handler=GetRoomsByType
    // Returns JSON
    //REminder write GetRoomsByType.cshtml
    public class GetRoomsByTypeModel : PageModel
    {
        private readonly HotelManagementService _hotelManagementService;

        public GetRoomsByTypeModel(HotelManagementService hotelManagementService)
        {
            _hotelManagementService = hotelManagementService;
        }

        public List<SelectListItem> RoomList { get; set; }

        public async Task<IActionResult> OnGetAsync(string type)
        {
            RoomList = await _hotelManagementService.GetRoomsByTypeAsync(type);
            return new JsonResult(RoomList);
        }
    }
}
