using EjercicioFinalOOP.Models;

namespace EjercicioFinalOOP.Interfaces;

public interface IFeeSystem
{
    List<InvoiceItem> CalculateServiceFees(IFacturable item);
}