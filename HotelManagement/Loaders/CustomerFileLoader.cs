using EjercicioFinalOOP.Excepciones;
using EjercicioFinalOOP.Interfaces;
using EjercicioFinalOOP.Models;

namespace EjercicioFinalOOP.Loaders
{
    public class CustomerFileLoader : ICustomerRepository
    {
        private string FilePath { get; set; }
        List<Customer> Customers { get; set; }

        public CustomerFileLoader(string filePath)
        {
            FilePath = filePath;
            Customers = new List<Customer>();
            // Leemos CSV de clientes, usando el separador ;
            using var reader = new StreamReader(FilePath);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                Customers.Add(new Customer(values[0], values[1], values[2], values[3], values[4], float.Parse(values[5])));
            }
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public void DeleteCustomer(string idNumber)
        {
            Customer customer = Customers.FirstOrDefault(c => c.IdNumber == idNumber) ?? throw new CustomerNotFoundException(idNumber);
            Customers.Remove(customer);
        }

        public List<Customer> GetAllCustomers()
        {
            return Customers;
        }

        public Customer GetCustomerById(string idNumber)
        {
            return Customers.FirstOrDefault(c => c.IdNumber == idNumber) ?? throw new CustomerNotFoundException(idNumber);
        }

        public void UpdateCustomer(Customer customer)
        {
            _ = Customers.FirstOrDefault(c => c.IdNumber == customer.IdNumber) ?? throw new CustomerNotFoundException(customer.IdNumber);
        }

    }

}
