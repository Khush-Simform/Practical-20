using System.Runtime.CompilerServices;
using WebApiCore.Data;
using WebApiCore.Models;

namespace WebApiCore.Services.CustomersServices
{
    
        public class UserServices : IUserServices
        {
            

            private readonly DataContext _context;
            public UserServices(DataContext context)
            {
                this._context = context;
            }
          
            public User GetSingleUser(string userName)
            {
                var user = _context.Users.FirstOrDefault(x => x.UserName == userName);
                if (user == null)
                {
                    return null;
                }
                return user;

            }

            public User AddUser(User users)
            {
                _context.Users.Add(users);
                _context.SaveChanges();
                return users;

            }
            public User UpdateUser(User user)
            {

                var userData =  _context.Users.FirstOrDefault(x => x.UserName == user.UserName);
                if (userData == null)
                {
                    return null;
                }

                userData.Token = user.Token;
               
                _context.SaveChanges();
                return userData;
            }


        }
    
}
