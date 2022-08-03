using API.Application.Interfaces.Generics;
using API.Domain.Models;

namespace API.Application.Interfaces
{
    public interface IApplicationNoticia : IGenericsApplication<Noticia>
    {
        Task<List<Noticia>> GetAllAtive();
        Task AddNoticia(Noticia noticia);
        Task UpdateNoticia(Noticia noticia);
        Task DeleteNoticia(Noticia noticia);
    }
}
