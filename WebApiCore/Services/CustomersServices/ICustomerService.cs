using WebApiCore.Models;

namespace WebApiCore.Services.CustomersServices
{
    public interface ICustomerService
    {
        Task<List<Customers>> GetAllCustomers();
        Task<Customers> GetSingleCustomer(int id);
        Task<Customers> AddCustomer(Customers customer);
        Task<Customers> UpdateCustomer(int id, Customers customer);
        Task<Customers> DeleteCustomer(int id);
        void Save();
    }
}
