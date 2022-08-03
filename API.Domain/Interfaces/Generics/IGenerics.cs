namespace API.Domain.Interfaces.Generics
{
    public interface IGenerics<TEntity> where TEntity : class
    {
        Task Add(TEntity obj);        
        Task Update(TEntity obj);        
        Task Delete(TEntity obj);
        Task<TEntity> GetById(int Id);
        Task<List<TEntity>> GetAll();        
    }
}
