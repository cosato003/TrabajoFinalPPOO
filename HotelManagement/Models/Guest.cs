// Namespace para las Clases Concretas
namespace EjercicioFinalOOP.Models
{
    public class Guest(string name = "", string idType = "", string idNumber = "", string phone = "")
        : Person(name, idType, idNumber, phone);

}
