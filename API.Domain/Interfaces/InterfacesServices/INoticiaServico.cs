using API.Domain.Models;

namespace API.Domain.Interfaces.InterfacesServices
{
    public interface INoticiaServico
    {
        Task<List<Noticia>> GetAllAtive();
        Task AddNoticia(Noticia noticia);
        Task UpdateNoticia(Noticia noticia);
        Task DeleteNoticia(Noticia noticia);
    }
}
