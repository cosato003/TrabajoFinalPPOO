// Namespace para las Clases Concretas

using EjercicioFinalOOP.Interfaces;

namespace EjercicioFinalOOP.Models
{
    public class ExecutiveRoom : Room, IFillable
    {
        public BedType BedType { get; set; }
        private readonly Minibar _minibarStatic = new Minibar(4, 2, 1, 2, 0);

        public Minibar Minibar { get; set; }


        public ExecutiveRoom(uint roomNumber = default, uint floor = default, float nightlyRate = default, BedType bedType = default) : base(roomNumber, floor, nightlyRate)
        {
            BedType = bedType;
            Minibar = _minibarStatic;
        }

        public void Refill(Interfaces.IFacturable item)
        {
            if (Guest == null)
            {
                throw new Exception("No hay un huésped asignado a la habitación.");
            }
            // Facturamos los productos que se reponen
            List<InvoiceItem> transactions = new List<InvoiceItem>();
            // Por cada producto que se reponga, se factura
            // item es el Actual Cuarto
            if (item is ExecutiveRoom)
            {
                if (Minibar.LiquorBottles < _minibarStatic.LiquorBottles)
                {
                    transactions.Add(new InvoiceItem(TypeOfInvoiceItem.AdditionalItem, "Botella de licor", 25000, _minibarStatic.LiquorBottles - Minibar.LiquorBottles )
                    {
                        Guest = this.Guest
                    });
                }
                if (Minibar.WaterBottles < _minibarStatic.WaterBottles)
                {
                    transactions.Add(new InvoiceItem(TypeOfInvoiceItem.AdditionalItem, "Botella de agua", 3500, _minibarStatic.WaterBottles - Minibar.WaterBottles )
                    {
                        Guest = this.Guest
                    });
                }
                if (Minibar.PersonalCareKits < _minibarStatic.PersonalCareKits)
                {
                    transactions.Add(new InvoiceItem(TypeOfInvoiceItem.AdditionalItem, "Kit de aseo personal", 9000, _minibarStatic.PersonalCareKits - Minibar.PersonalCareKits )
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
            }
            else
            {
                throw new Exception("El cuarto no es una habitación ejecutiva.");
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

}
