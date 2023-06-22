using Microsoft.EntityFrameworkCore;
using System;
using WebApiCore.Data;

namespace WebApiCore.Services.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal DataContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }
        public async Task<TEntity> GetById(object id)
        {
            return await dbSet.FindAsync(id);
        }
        public async Task<TEntity> Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            
            return entity;
        }
        public async Task<TEntity> Delete(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            return Delete(entityToDelete);
        }
        public TEntity Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            return entityToDelete;
        }
        public async Task<TEntity> Update(object id, TEntity entityToUpdate)
        {
            TEntity data = await dbSet.FindAsync(id);
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            return entityToUpdate;
        }        
    }
}
