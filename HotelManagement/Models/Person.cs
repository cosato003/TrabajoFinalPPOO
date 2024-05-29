// Namespace para las Clases Abstractas
namespace EjercicioFinalOOP.Models;

public abstract class Person(string name, string idType, string idNumber, string phone)
{
    public string Name { get; set; } = name;
    public string IdType { get; set; } = idType;
    public string IdNumber { get; set; } = idNumber;
    public string Phone { get; set; } = phone;
}