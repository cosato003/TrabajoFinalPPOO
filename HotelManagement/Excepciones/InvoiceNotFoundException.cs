// Namespace para las Excepciones
namespace EjercicioFinalOOP.Excepciones;

public class InvoiceNotFoundException(int id) : Exception($"La factura con el ID {id} no fue encontrada.");