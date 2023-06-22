namespace WebApiCore.Services.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(object id);
        Task<TEntity> Insert(TEntity obj);
        Task<TEntity> Update(object id, TEntity obj);
        Task<TEntity> Delete(object obj);
    }
}
