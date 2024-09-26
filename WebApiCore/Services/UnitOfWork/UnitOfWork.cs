using System;
using WebApiCore.Data;
using WebApiCore.Models;
using WebApiCore.Services.CustomersServices;
using WebApiCore.Services.GenericRepository;

namespace WebApiCore.Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _context;
        public IGenericRepository<Customers> customerRepository;
        public IGenericRepository<User> userRepository;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IGenericRepository<Customers> CustomerRepository
        {
            get
            {

                if (this.customerRepository == null)
                {
                    this.customerRepository = new GenericRepository<Customers>(_context);
                }
                return customerRepository;
            }
        }
        public IGenericRepository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(_context);
                }
                return userRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
