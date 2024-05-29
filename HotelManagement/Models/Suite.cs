// Namespace para las Clases Concretas

using EjercicioFinalOOP.Interfaces;

namespace EjercicioFinalOOP.Models;

public class Suite : Room, IFillable
{
    public BedType BedType { get; set; }
    private readonly Minibar _minibarStatic = new Minibar(4, 2, 3, 4, 1);
    public int Bathrobes { get; set; }

    public Minibar Minibar { get; set; }

    public Suite(uint roomNumber = default, uint floor= 1, float nightlyRate= default, BedType bedType= default)
        : base(roomNumber, floor, nightlyRate)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(floor);
        BedType = bedType;
        Minibar = _minibarStatic;
        Bathrobes = 0;
    }

    public void Refill(Interfaces.IFacturable item)
    {
        if (Guest == null)
        {
            throw new Exception("No hay un huésped asignado a la habitación.");
        }
        // Facturamos los productos que se reponen
        var transactions = new List<InvoiceItem>();
        if (transactions == null) throw new ArgumentNullException(nameof(transactions));
        // Por cada producto que se reponga, se factura
        // item es el Actual Cuarto
        if (item is Suite)
        {
            if (Minibar.LiquorBottles < _minibarStatic.LiquorBottles)
            {
                transactions.Add(new InvoiceItem(TypeOfInvoiceItem.AdditionalItem, "Botella de licor", 25000, _minibarStatic.LiquorBottles - Minibar.LiquorBottles)
        {
            Guest = this.Guest
        });
            }
            if (Minibar.WaterBottles < _minibarStatic.WaterBottles)
            {
                transactions.Add(new InvoiceItem(TypeOfInvoiceItem.AdditionalItem, "Botella de agua", 3500, _minibarStatic.WaterBottles - Minibar.WaterBottles)
        {
            Guest = this.Guest
        });
            }
            if (Minibar.PersonalCareKits < _minibarStatic.PersonalCareKits)
            {
                transactions.Add(new InvoiceItem(TypeOfInvoiceItem.AdditionalItem, "Kit de aseo personal", 9000, _minibarStatic.PersonalCareKits - Minibar.PersonalCareKits)
        {
            Guest = this.Guest
        });
            }
            if (Minibar.Sodas < _minibarStatic.Sodas)
            {
                transactions.Add(new InvoiceItem(TypeOfInvoiceItem.AdditionalItem, "Gaseosa", 3000, _minibarStatic.Sodas - Minibar.Sodas)
        {
            Guest = this.Guest
        });
            }
            if (Minibar.WineBottles < _minibarStatic.WineBottles)
            {
                transactions.Add(new InvoiceItem(TypeOfInvoiceItem.AdditionalItem, "Botella de vino", 50000, _minibarStatic.WineBottles - Minibar.WineBottles)
        {
            Guest = this.Guest
        });
            }
            if (Bathrobes < 2)
            {
                transactions.Add(new InvoiceItem(TypeOfInvoiceItem.AdditionalItem, "Bata de baño", 70000, 2 - Bathrobes)
        {
            Guest = this.Guest
        });
            }
        }
        else
        {
            throw new Exception("El cuarto no es una suite.");
        }
    }

    public override void AddTransaction(InvoiceItem item)
    {
        item.Invoice = Invoice;

        Transactions.Add(item);
    }

    protected override void CheckoutEvent()
    {
        Refill(this);
    }

}