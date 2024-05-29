// Namespace para los Repositorios

using EjercicioFinalOOP.Models;

namespace EjercicioFinalOOP.Interfaces;

public interface ICustomerRepository
{
    Customer GetCustomerById(string idNumber);
    List<Customer> GetAllCustomers();
    void AddCustomer(Customer customer);
    void UpdateCustomer(Customer customer);
    void DeleteCustomer(string idNumber);
}