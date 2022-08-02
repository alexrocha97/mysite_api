using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Service;
using Api.Domain.Models;

namespace Api.Domain.Services
{
    public class ServiceCliente : ServiceBase<Artigo>, IServiceArtigo
    {
        public readonly IRepositoryArtigo _repositoryArtigo;
        public ServiceCliente(IRepositoryArtigo repositoryArtigo) : base(repositoryArtigo)
        {
            _repositoryArtigo = repositoryArtigo;
        }
    }
}
