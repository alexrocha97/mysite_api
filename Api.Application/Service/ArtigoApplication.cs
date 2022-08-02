using Api.Application.Interfaces;
using Api.Application.Models;
using Api.Domain.Interfaces.Service;

namespace Api.Application.Service
{
    public class ArtigoApplication : IArtigoApplication
    {
        private readonly IServiceArtigo _serviceArtigo;
        public ArtigoApplication(IServiceArtigo serviceArtigo)
        {
            _serviceArtigo = serviceArtigo;
        }

        public Task<IEnumerable<Artigo>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Artigo> GetById(int Id)
        {
            throw new NotImplementedException();
        }
        
        public Task<Artigo> Create(Artigo artigo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Artigo> Update(Artigo artigo)
        {
            throw new NotImplementedException();
        }
    }
}
