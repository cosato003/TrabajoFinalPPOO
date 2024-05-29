using EjercicioFinalOOP.Models;

namespace EjercicioFinalOOP.Interfaces;

public interface IFacturable
{
    void AddTransaction(InvoiceItem item);
    List<Models.InvoiceItem> GetTransactions();
}