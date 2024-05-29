// Namespace para los Repositorios

using EjercicioFinalOOP.Models;

namespace EjercicioFinalOOP.Interfaces;

public interface IInvoiceRepository
{
    Invoice GetInvoiceById(int id);
    List<Invoice> GetAllInvoices();
    void AddInvoice(Invoice invoice);
    void UpdateInvoice(Invoice invoice);
    void DeleteInvoice(int id);
}