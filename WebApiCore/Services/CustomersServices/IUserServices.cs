using WebApiCore.Models;

namespace WebApiCore.Services.CustomersServices
{
    public interface IUserServices
    {
        User GetSingleUser(string userName);
        User AddUser(User users);
        User UpdateUser(User user);
    }
}
