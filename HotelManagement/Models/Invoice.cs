// Namespace para las Clases Concretas
namespace EjercicioFinalOOP.Models;

public class Invoice
{
    public List<InvoiceItem> Items { get; set; } = new();
    public float TotalAmmount { get; set; }

    public float Discount { get; set; }

    public int Id { get; set; }

  

    public Invoice( float discount = 0.0f,  
        int id = default)
    {
        Discount = TotalAmmount * discount;
        Id = id;
    }

    public void CalculateTotal()
    {
        TotalAmmount = (Items ?? throw new InvalidOperationException()).Sum(x => x.Quantity * x.UnitCost);
        var elem = Items!.FirstOrDefault(x =>  x.Description == "Descuento");
        Discount = elem != null ? elem.Quantity * elem.UnitCost : 0;
    }

    public float GetTotalAmount()
    {
        CalculateTotal();
        
        return TotalAmmount - Discount;
    }

    public List<InvoiceItem> GetItems()
    {
        return Items;
    }

}