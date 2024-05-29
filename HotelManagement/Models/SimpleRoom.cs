// Namespace para las Clases Concretas
namespace EjercicioFinalOOP.Models;

public class SimpleRoom(
    uint roomNumber = default,
    uint floor = default,
    float nightlyRate = default,
    BedType bedType = default)
    : Room(roomNumber, floor, nightlyRate)
{
    public BedType BedType { get; set; } = bedType;

    public override void AddTransaction(InvoiceItem item)
    {
        throw new Exception("No se pueden agregar transacciones a una habitación sencilla.");
    }

    protected override void CheckoutEvent()
    {
    }
}