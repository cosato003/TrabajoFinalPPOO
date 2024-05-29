using EjercicioFinalOOP.Interfaces;

// Namespace para las Clases Abstractas
namespace EjercicioFinalOOP.Models;

public abstract class Room(uint roomNumber, uint floor, float nightlyRate) : IFacturable
{
    public uint RoomNumber { get; set; } = roomNumber;
    public uint Floor { get; set; } = floor;
    public float NightlyRate { get; set; } = nightlyRate;
    public bool Occupied { get; set; } = false;
    public uint Capacity { get; set; } = 1;
    public List<InvoiceItem> Transactions { get; set; } = new();
    public Person? Guest { get; set; } = null;

    public DateTime OutDate { get; set; }
    public DateTime CheckinDate { get; set; }
    
    public Invoice? Invoice { get; set; }


    public void CheckIn(Person guest, DateTime outDate, Invoice invoice)
    {
        if (!Occupied)
        {
            Occupied = true;
            Guest = guest;
            OutDate = outDate;
            CheckinDate = DateTime.Today;
            Invoice = invoice;
        }
        else
        {
            throw new Exception("La habitación ya está ocupada.");
        }

    }
    public void CheckOut()
    {
        if (!Occupied)
        {
            throw new Exception("La habitación ya está desocupada.");
        }
        if (Guest == null)
        {
            throw new Exception("No hay un huésped asignado a la habitación.");
        }
        CheckoutEvent();

        var daysElapsed =  OutDate - CheckinDate ;
        Transactions.Add(new InvoiceItem(TypeOfInvoiceItem.Room, $"Habitación {RoomNumber} - {Floor}", NightlyRate, Math.Max(daysElapsed.Days,1))
        {
            Guest = this.Guest,
            Invoice = this.Invoice,
        });
        Occupied = false;
        Guest = null;

    }

    protected abstract void CheckoutEvent();
    public abstract void AddTransaction(InvoiceItem item);

    public List<InvoiceItem> GetTransactions()
    {
        return Transactions;
    }
}