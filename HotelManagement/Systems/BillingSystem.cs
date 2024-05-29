// Namespace para las Clases Concretas

using EjercicioFinalOOP.Models;

namespace EjercicioFinalOOP.Systems
{
    public class BillingSystem
    {
        private const float HotelInsuranceRate = 0.025f;
        private const float IvaRate = 0.19f;

        public void AppendInvoices(List<InvoiceItem> transactions)
        {
            var invoice =  transactions[0].Invoice;
            transactions.ForEach(t => t.Invoice = invoice);

            var RoomTotal = transactions.Sum(t => t.TotalCost);

            var TotalAmount = RoomTotal;
            var HotelInsurance = TotalAmount * HotelInsuranceRate;
            var Iva = TotalAmount * IvaRate;
            var tieneSeguro = transactions[0].Guest is Customer && transactions.Any(t => t.Description == "Seguro hotelero");
            var tieneIva = transactions[0].Guest is Customer && transactions.Any(t => t.Description == "IVA");
            if (tieneSeguro)
            {
                transactions.Add(new InvoiceItem(TypeOfInvoiceItem.AdditionalItem, "Seguro hotelero", HotelInsurance, 1)
                {
                    Guest = transactions[0].Guest
                });
            }
            


            if (transactions[0].Guest is Customer && !tieneIva)
            {
                transactions.Add(new InvoiceItem(TypeOfInvoiceItem.AdditionalItem, "IVA", Iva, 1)
                {
                    Guest = transactions[0].Guest
                });
            }

            if (transactions[0].Guest is Customer customer)
            {
                var descuento = invoice.TotalAmmount * (100f - customer.Discount)/100f;
                transactions.Add(new InvoiceItem(TypeOfInvoiceItem.AdditionalItem, "Descuento", -descuento, 1)
                {
                    Guest = transactions[0].Guest
                });
                invoice.TotalAmmount -= descuento;
            }
            
            transactions.ForEach(x => x.Invoice = invoice);

        }
    }

}
