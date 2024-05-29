// Namespace para las Excepciones
namespace EjercicioFinalOOP.Excepciones;

public class CustomerNotFoundException(string idNumber)
    : Exception($"El cliente con el número de identificación {idNumber} no fue encontrado.");