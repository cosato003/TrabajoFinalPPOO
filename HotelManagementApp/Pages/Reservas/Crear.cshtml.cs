using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EjercicioFinalOOP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagementApp.Pages.Reservas
{
    public class CrearModel : PageModel
    {
        private readonly HotelManagementService _hotelManagementService;

        public CrearModel(HotelManagementService hotelManagementService)
        {
            _hotelManagementService = hotelManagementService;
        }

        [BindProperty]
        public string GuestId { get; set; }

        [BindProperty]
        public string NewGuestName { get; set; }

        [BindProperty]
        public string NewGuestIdType { get; set; }

        [BindProperty]
        public string NewGuestIdNumber { get; set; }

        [BindProperty]
        public string NewGuestPhone { get; set; }

        [BindProperty]
        public string RoomType { get; set; }

        [BindProperty]
        public uint RoomId { get; set; }

        [BindProperty]
        public DateTime CheckInDate { get; set; }

        [BindProperty]
        public DateTime CheckOutDate { get; set; }

        public List<SelectListItem> GuestList { get; set; }
        public List<SelectListItem> RoomTypeList { get; set; }
        public List<SelectListItem> RoomList { get; set; }

        public async Task OnGetAsync()
        {
            var guests = await _hotelManagementService.GetGuestsAsync();
            GuestList = new List<SelectListItem>();
            foreach (var guest in guests)
            {
                GuestList.Add(new SelectListItem { Value = guest.IdNumber, Text = $"{guest.Name} ({guest.IdNumber})" });
            }

            RoomTypeList = new List<SelectListItem>
            {
                new SelectListItem { Value = "Simple", Text = "Habitación Sencilla" },
                new SelectListItem { Value = "Executive", Text = "Habitación Ejecutiva" },
                new SelectListItem { Value = "Suite", Text = "Suite" }
            };

            RoomList = new List<SelectListItem>();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            OnGetAsync();
            Guest guest = null;

            if (!string.IsNullOrEmpty(GuestId))
            {
                guest = await _hotelManagementService.GetGuestAsync(GuestId);
                if (guest == null)
                {
                    ModelState.AddModelError(string.Empty, "Huésped no encontrado.");
                    return Page();
                }
            }
            else if (!string.IsNullOrEmpty(NewGuestName) && !string.IsNullOrEmpty(NewGuestIdType) && !string.IsNullOrEmpty(NewGuestIdNumber) && !string.IsNullOrEmpty(NewGuestPhone))
            {
                guest = new Guest
                {
                    Name = NewGuestName,
                    IdType = NewGuestIdType,
                    IdNumber = NewGuestIdNumber,
                    Phone = NewGuestPhone
                };

                await _hotelManagementService.AddGuestAsync(guest);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Debe proporcionar información del huésped existente o registrar un nuevo huésped.");
                return Page();
            }

            try
            {
                await _hotelManagementService.MakeReservationAsync(RoomId, guest, CheckInDate, CheckOutDate);
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
