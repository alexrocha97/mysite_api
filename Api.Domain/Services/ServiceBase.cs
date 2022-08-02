using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Service;
using Api.Domain.Models;

namespace Api.Domain.Services
{
    public abstract class ServiceBase<TEntity> : IDisposable, IServiceBase<Artigo> where TEntity : class
    {
        private readonly IRepositoryBase<Artigo> _repositoryArtigo;
        protected ServiceBase(IRepositoryBase<Artigo> repositoryArtigo)
        {
            _repositoryArtigo = repositoryArtigo;
        }

        public virtual void Add(Artigo obj)
        {
            _repositoryArtigo.Add(obj);
        }

        public Task<IEnumerable<Artigo>> GetAll()
        {
            return _repositoryArtigo.GetAll();
        }

        public Artigo GetById(int id)
        {
            return _repositoryArtigo.GetById(id);
        }

        public void Remove(Artigo obj)
        {
            _repositoryArtigo.Remove(obj);
        }

        public void Update(Artigo obj)
        {
            _repositoryArtigo.Update(obj);
        }

        public virtual void Dispose()
        {
            _repositoryArtigo.Dispose();
        }
    }
}
