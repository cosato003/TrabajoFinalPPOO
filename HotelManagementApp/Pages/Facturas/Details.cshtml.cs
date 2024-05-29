using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EjercicioFinalOOP.Models;
using System.Threading.Tasks;

namespace HotelManagementApp.Pages.Facturas
{
    public class DetailsModel : PageModel
    {
        private readonly HotelManagementService _hotelManagementService;

        public DetailsModel(HotelManagementService hotelManagementService)
        {
            _hotelManagementService = hotelManagementService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Invoice Factura { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Factura = await _hotelManagementService.GetInvoiceAsync(Id);
            Factura.GetTotalAmount();

            if (Factura == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}