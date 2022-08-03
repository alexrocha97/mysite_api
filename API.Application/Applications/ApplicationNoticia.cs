using API.Application.Interfaces;
using API.Domain.Interfaces;
using API.Domain.Interfaces.InterfacesServices;
using API.Domain.Models;

namespace API.Application.Applications
{
    public class ApplicationNoticia : IApplicationNoticia
    {
        private readonly INoticia _noticia;
        private readonly INoticiaServico _serviceNoticia;
        public ApplicationNoticia(INoticia noticia, INoticiaServico serviceNoticia)
        {
            _noticia = noticia;
            _serviceNoticia = serviceNoticia;
        }

        public async Task Add(Noticia obj)
        {
            await _noticia.Add(obj);
        }

        public async Task AddNoticia(Noticia noticia)
        {
            await _serviceNoticia.AddNoticia(noticia);
        }

        public async Task Delete(Noticia obj)
        {
            await _noticia.Delete(obj);
        }

        // Indisponivel
        public async Task DeleteNoticia(Noticia noticia)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Noticia>> GetAll()
        {
            return await _noticia.GetAll();
        }

        public async Task<List<Noticia>> GetAllAtive()
        {
           return await _serviceNoticia.GetAllAtive();
        }

        public async Task<Noticia> GetById(int Id)
        {
            return await _noticia.GetById(Id);
        }

        public async Task Update(Noticia obj)
        {
            await _noticia.Update(obj);
        }

        public async Task UpdateNoticia(Noticia noticia)
        {
            await _serviceNoticia.UpdateNoticia(noticia);
        }
    }
}
