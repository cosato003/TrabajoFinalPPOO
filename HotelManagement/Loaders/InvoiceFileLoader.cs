using EjercicioFinalOOP.Excepciones;
using EjercicioFinalOOP.Interfaces;
using EjercicioFinalOOP.Models;

namespace EjercicioFinalOOP.Loaders
{
    public class InvoiceFileLoader : IInvoiceRepository
    {
        private string FilePath { get; set; }
        List<Invoice> Invoices { get; set; }

        public InvoiceFileLoader(string filePath)
        {
            FilePath = filePath;
            Invoices = new List<Invoice>();
            // Leemos CSV de facturas, usando el separador ;
            using (var reader = new StreamReader(FilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    List<InvoiceItem> items = new List<InvoiceItem>();
                    for (int i = 1; i < values.Length; i++)
                    {
                        var itemValues = values[i].Split(',');
                        items.Add(new InvoiceItem((TypeOfInvoiceItem)Enum.Parse(typeof(TypeOfInvoiceItem), itemValues[0]), itemValues[1], float.Parse(itemValues[2]), int.Parse(itemValues[3])));
                    }
                    Invoices.Add(new Invoice()
                    {
                        Items = items,
                    });
                }
            }
        }

        public void AddInvoice(Invoice invoice)
        {
            Invoices.Add(invoice);
        }

        public void DeleteInvoice(int id)
        {
            Invoice invoice = Invoices.FirstOrDefault(i => i.Id == id) ?? throw new InvoiceNotFoundException(id);
            Invoices.Remove(invoice);
        }

        public List<Invoice> GetAllInvoices()
        {
            return Invoices;
        }

        public Invoice GetInvoiceById(int id)
        {
            return Invoices.FirstOrDefault(i => i.Id == id) ?? throw new InvoiceNotFoundException(id);
        }

        public void UpdateInvoice(Invoice invoice)
        {
            _ = Invoices.FirstOrDefault(i => i.Id == invoice.Id) ?? throw new InvoiceNotFoundException(invoice.Id);
        }

    }

}
