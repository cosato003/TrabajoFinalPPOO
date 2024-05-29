using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EjercicioFinalOOP.Models;
using System.Threading.Tasks;

namespace HotelManagementApp.Pages.Clientes
{
    public class CrearModel : PageModel
    {
        private readonly HotelManagementService _hotelManagementService;

        public CrearModel(HotelManagementService hotelManagementService)
        {
            _hotelManagementService = hotelManagementService;
        }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string IdType { get; set; }

        [BindProperty]
        public string IdNumber { get; set; }

        [BindProperty]
        public string Phone { get; set; }

        [BindProperty]
        public string LoyaltyCode { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customer = new Customer
            {
                Name = Name,
                IdType = IdType,
                IdNumber = IdNumber,
                Phone = Phone,
                LoyaltyCode = LoyaltyCode
            };

            await _hotelManagementService.AddCustomerAsync(customer);

            return RedirectToPage("/Index");
        }
    }
}