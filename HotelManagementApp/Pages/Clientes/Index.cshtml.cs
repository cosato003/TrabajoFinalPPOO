using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EjercicioFinalOOP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagementApp.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        private readonly HotelManagementService _hotelManagementService;

        public IndexModel(HotelManagementService hotelManagementService)
        {
            _hotelManagementService = hotelManagementService;
        }

        public IList<Customer> Clientes { get; set; }

        public async Task OnGetAsync()
        {
            Clientes = await _hotelManagementService.GetCustomersAsync();
        }

        public async Task<IActionResult> OnPostActualizarAsync([FromBody] CustomerUpdateModel customerUpdate)
        {
            if (ModelState.IsValid)
            {
                var customer = await _hotelManagementService.GetCustomerAsync(customerUpdate.IdNumber);
                if (customer != null)
                {
                    customer.Discount = customerUpdate.Discount;
                    await _hotelManagementService.UpdateCustomerAsync(customer);
                    return new JsonResult(new { success = true });
                }
                return new JsonResult(new { success = false, message = "Cliente no encontrado" });
            }
            return new JsonResult(new { success = false, message = "Datos inválidos" });
        }

        public class CustomerUpdateModel
        {
            public string IdNumber { get; set; }
            public float Discount { get; set; }
        }
    }
}
