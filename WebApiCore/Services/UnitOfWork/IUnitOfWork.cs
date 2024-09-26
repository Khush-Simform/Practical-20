using WebApiCore.Models;
using WebApiCore.Services.CustomersServices;
using WebApiCore.Services.GenericRepository;

namespace WebApiCore.Services.UnitOfWork
{
    public interface IUnitOfWork :IDisposable
    {
        IGenericRepository<Customers> CustomerRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        void Save();
    }
}
