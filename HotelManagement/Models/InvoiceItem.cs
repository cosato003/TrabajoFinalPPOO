

// Namespace para las Clases Concretas
namespace EjercicioFinalOOP.Models
{
    public class InvoiceItem
    {
        public TypeOfInvoiceItem Type { get; set; }
        public Person Guest { get; set; }
        public string Description { get; set; }
        public float UnitCost { get; set; }
        public int Quantity { get; set; }
        public float TotalCost { get; set; }
        public int Id { get; set; } = 0;
        public DateTime When { get; set; } = DateTime.Now;
        public Invoice Invoice { get; set; }

        public InvoiceItem(TypeOfInvoiceItem type = default, string description = "", float unitCost = default, int quantity = default)
        {
            Type = type;
            Description = description;
            UnitCost = unitCost;
            Quantity = quantity;
            TotalCost = unitCost * quantity;
            Invoice = null;
        }

        public void Verify()
        {
             _ = Guest ?? throw new ArgumentException(nameof(Guest));
        } 


    }

}
