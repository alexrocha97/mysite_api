using Api.Application.Models;

namespace Api.Application.Interfaces
{
    public interface IArtigoApplication
    {
        Task<IEnumerable<Artigo>> GetAll();
        Task<Artigo> GetById(int Id);
        Task<Artigo> Create(Artigo artigo);
        Task<Artigo> Update(Artigo artigo);
        Task<bool> Delete(int Id);
    }
}
