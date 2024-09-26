using Microsoft.EntityFrameworkCore;
using WebApiCore.Data;
using WebApiCore.Models;
namespace WebApiCore.Services.CustomersServices
{
    public class CustomerService : ICustomerService
    {
        //private static List<Customers> customers = new List<Customers>
        //    {
        //        new Customers {
        //            CustomerId= 1 ,
        //            UserName = "jenish1212",
        //            Email = "jenishraiyani74@gmail.com",
        //            MobilePhone="7573528845",
        //            Password = "1212" }, 
        //        new Customers {
        //            CustomerId= 2 ,
        //            UserName = "sumanjha",
        //            Email = "sumnajha1212@gmail.com",
        //            MobilePhone="8855445588",
        //            Password = "02185" },
        //    };

        private readonly DataContext _context;
        public CustomerService(DataContext context)
        {
            this._context = context;
        }
        public async Task<List<Customers>> GetAllCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return customers;
        }
        public async Task<Customers> GetSingleCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return null;
            }
            return customer;
        }
       
        public async Task<Customers> AddCustomer(Customers customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
       
        public async Task<Customers> UpdateCustomer(int id, Customers customer)
        {

            var customerData = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return null;
            }

            customerData.UserName = customer.UserName;
            customerData.Email = customer.Email;
            customerData.MobilePhone = customer.MobilePhone;
            customerData.Password = customer.Password;
            await _context.SaveChangesAsync();
            return customerData;
        }
        public async Task<Customers>DeleteCustomer(int id)
        {
            var customerData = await _context.Customers.FindAsync(id);
            if (customerData == null)
            {
                return null;
            }
            _context.Customers.Remove(customerData);
            await _context.SaveChangesAsync();
            return customerData;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
