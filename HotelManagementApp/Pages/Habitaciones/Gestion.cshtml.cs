using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EjercicioFinalOOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HotelManagementApp.Pages.Habitaciones
{
    public class GestionModel : PageModel
    {
        private readonly HotelManagementService _hotelManagementService;

        public GestionModel(HotelManagementService hotelManagementService)
        {
            _hotelManagementService = hotelManagementService;
        }

        [BindProperty(SupportsGet = true)]
        public uint Id { get; set; }

        public Room Room { get; set; }

        public Minibar Minibar { get; set; }

        public List<InvoiceItem> Transactions { get; set; }

        public float TotalGasto { get; set; }

        [BindProperty]
        public MinibarItem SelectedMinibarItem { get; set; } = MinibarItem.LiquorBottle;

        [BindProperty]
        public int Quantity { get; set; }

        [BindProperty] public string RestaurantItem { get; set; } = "Desayuno";

        [BindProperty] public bool RoomService { get; set; } = true;

        [BindProperty] public int LaundryQuantity { get; set; } = 0;

        [BindProperty] public bool Ironing { get; set; } = true;

        public async Task<IActionResult> OnGetAsync()
        {
            Room = await _hotelManagementService.GetRoomAsync(Id);

            if (Room == null)
            {
                return NotFound();
            }

            Transactions = Room.GetTransactions();
            TotalGasto = Transactions.Sum(t => t.TotalCost);

            if (Room is ExecutiveRoom executiveRoom)
            {
                Minibar = executiveRoom.Minibar;
            }
            else if (Room is Suite suite)
            {
                Minibar = suite.Minibar;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostMinibarAsync()
        {
            Room = await _hotelManagementService.GetRoomAsync(Id);

            if (Room == null)
            {
                return NotFound();
            }

            Transactions = Room.GetTransactions();
            TotalGasto = Transactions.Sum(t => t.TotalCost);

            if (Room is ExecutiveRoom executiveRoom)
            {
                Minibar = executiveRoom.Minibar;
            }
            else if (Room is Suite suite)
            {
                Minibar = suite.Minibar;
            }
            try
            {
                switch (SelectedMinibarItem)
                {
                    case MinibarItem.LiquorBottle:
                        Minibar.LiquorBottles = Math.Max(0, Minibar.LiquorBottles - Quantity);
                        break;
                    case MinibarItem.WaterBottle:
                        Minibar.WaterBottles = Math.Max(0, Minibar.WaterBottles - Quantity);
                        break;
                    case MinibarItem.PersonalCareKit:
                        Minibar.PersonalCareKits = Math.Max(0, Minibar.PersonalCareKits - Quantity);
                        break;
                    case MinibarItem.Soda:
                        Minibar.Sodas = Math.Max(0, Minibar.Sodas - Quantity);
                        break;
                    case MinibarItem.WineBottle:
                        Minibar.WineBottles = Math.Max(0, Minibar.WineBottles - Quantity);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }


                await _hotelManagementService.ReduceMinibarItemAsync(Id, SelectedMinibarItem, Quantity);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostRestauranteAsync()
        {
            Room = await _hotelManagementService.GetRoomAsync(Id);

            if (Room == null)
            {
                return NotFound();
            }

            Transactions = Room.GetTransactions();
            TotalGasto = Transactions.Sum(t => t.TotalCost);

            if (Room is ExecutiveRoom executiveRoom)
            {
                Minibar = executiveRoom.Minibar;
            }
            else if (Room is Suite suite)
            {
                Minibar = suite.Minibar;
            }
            if (ModelState.ContainsKey("RestaurantItem"))
            {
                var modelValidationState = ModelState["RestaurantItem"]!.ValidationState;
                if (modelValidationState != ModelValidationState.Valid)
                {
                    return Page();
                }
            }

            try
            {
                await _hotelManagementService.AddOrderAsync(RestaurantItem, Id, RoomService);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostLavanderiaAsync()
        {
            Room = await _hotelManagementService.GetRoomAsync(Id);

            if (Room == null)
            {
                return NotFound();
            }

            Transactions = Room.GetTransactions();
            TotalGasto = Transactions.Sum(t => t.TotalCost);

            if (Room is ExecutiveRoom executiveRoom)
            {
                Minibar = executiveRoom.Minibar;
            }
            else if (Room is Suite suite)
            {
                Minibar = suite.Minibar;
            }

            try
            {
                await _hotelManagementService.AddLaundryServiceAsync(Id, LaundryQuantity, Ironing);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            return Page();
        }
    }
}
