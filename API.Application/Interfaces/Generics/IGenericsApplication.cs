namespace API.Application.Interfaces.Generics
{
    public interface IGenericsApplication<TEntity> where TEntity : class
    {
        Task Add(TEntity obj);        
        Task Update(TEntity obj);        
        Task Delete(TEntity obj);
        Task<TEntity> GetById(int Id);
        Task<List<TEntity>> GetAll();  
    }
}
