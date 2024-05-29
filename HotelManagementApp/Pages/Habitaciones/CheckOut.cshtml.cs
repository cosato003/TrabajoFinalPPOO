using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace HotelManagementApp.Pages.Habitaciones
{
    public class CheckOutModel : PageModel
    {
        private readonly HotelManagementService _hotelManagementService;

        public CheckOutModel(HotelManagementService hotelManagementService)
        {
            _hotelManagementService = hotelManagementService;
        }

        [BindProperty(SupportsGet = true)]
        public uint Id { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _hotelManagementService.CheckOutAsync(Id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}
