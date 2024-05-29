using Microsoft.AspNetCore.Mvc.RazorPages;
using EjercicioFinalOOP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagementApp.Pages.Facturas
{
    public class IndexModel : PageModel
    {
        private readonly HotelManagementService _hotelManagementService;

        public IndexModel(HotelManagementService hotelManagementService)
        {
            _hotelManagementService = hotelManagementService;
        }

        public List<Invoice> Facturas { get; set; }

        public async Task OnGetAsync()
        {
            Facturas = await _hotelManagementService.GetInvoicesAsync();
            Facturas.ForEach(x => x.GetTotalAmount());
        }
    }
}