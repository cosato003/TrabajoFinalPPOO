using EjercicioFinalOOP.Interfaces;
using EjercicioFinalOOP.Models;

// Namespace para las Clases Concretas
namespace EjercicioFinalOOP.Systems;

public class RestaurantSystem : IFeeSystem
{
    private Dictionary<string, float> Menu { get; set; } = new()
    {
        { "Desayuno", 15000 },
        { "Almuerzo", 25000 },
        { "Cena", 20000 }
    };

    private List<InvoiceItem> Transactions { get; set; } = new();

    public void AddTransaction(InvoiceItem item)
    {
        Transactions.Add(item);
    }

    public List<InvoiceItem> CalculateServiceFees(IFacturable item)
    {
        if (item is Room room)
        {
            const float roomServiceFee = 5000;
            item.AddTransaction(new InvoiceItem(TypeOfInvoiceItem.AdditionalItem, "Servicio a la habitación", roomServiceFee, 1)
            {
                Guest = room.Guest ?? throw new Exception("No hay un huésped asignado a la habitación.")
            });
        }
        else
        {
            throw new Exception("El item no es una habitación.");
        }
        return Transactions;
    }

    public void AddOrder(string item, Room room)
    {
        if (Menu.TryGetValue(item, out float value))
        {
            room.AddTransaction(new InvoiceItem(TypeOfInvoiceItem.Restaurant, item, value, 1)
            {
                Guest = room.Guest ?? throw new Exception("No hay un huésped asignado a la habitación.")
            });
        }
        else
        {
            throw new Exception("El item no se encuentra en el menú.");
        }
    }


}