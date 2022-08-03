using System.Linq.Expressions;
using API.Domain.Interfaces.Generics;
using API.Domain.Models;

namespace API.Domain.Interfaces
{
    public interface INoticia : IGenerics<Noticia>
    {
        Task<List<Noticia>> GetAllNoticia(Expression<Func<Noticia, bool>> exNoticia);
    }
}
