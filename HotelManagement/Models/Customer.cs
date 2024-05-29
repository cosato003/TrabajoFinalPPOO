// Namespace para las Clases Concretas
namespace EjercicioFinalOOP.Models;

public class Customer(
    string name = "",
    string idType = "",
    string idNumber = "",
    string phone = "",
    string loyaltyCode = "",
    float discount = 0.0f)
    : Person(name, idType, idNumber, phone)
{
    public string LoyaltyCode { get; set; } = loyaltyCode;
    public float Discount { get; set; } = discount;
}