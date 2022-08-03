using System.Linq.Expressions;
using API.Domain.Interfaces;
using API.Domain.Models;
using API.Infra.Configuration;
using API.Infra.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace API.Infra.Repository
{
    public class RepositoryNoticia : RepositoryGenerics<Noticia>, INoticia
    {
        private readonly DbContextOptions<Contexto> _optionsBuilder;
        public RepositoryNoticia()
        {
            _optionsBuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<Noticia>> GetAllNoticia(Expression<Func<Noticia, bool>> exNoticia)
        {
            using(var banco = new Contexto(_optionsBuilder))
            {
                return await banco.Noticia.Where(exNoticia).AsNoTracking().ToListAsync();
            }
        }
    }
}
