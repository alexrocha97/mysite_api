using API.Domain.Interfaces;
using API.Domain.Interfaces.InterfacesServices;
using API.Domain.Models;

namespace API.Domain.Service
{
    public class NoticiaService : INoticiaServico
    {
        private readonly INoticia _noticia;
        public NoticiaService(INoticia noticia)
        {
            _noticia = noticia;
        }

        public async Task AddNoticia(Noticia noticia)
        {
            var validarTitulo = noticia.ValidaPropriedadeString(noticia.Titulo, "Titulo"); 
            var validarInformacoes = noticia.ValidaPropriedadeString(noticia.Informacao, "Informacoes"); 
            if(validarTitulo && validarInformacoes)
            {
                noticia.DataAlteracao = DateTime.Now;
                noticia.DataCadastro = DateTime.Now;
                noticia.Ativo = true;
                await _noticia.Add(noticia);
            }
        }

        public async Task UpdateNoticia(Noticia noticia)
        {
            var validarTitulo = noticia.ValidaPropriedadeString(noticia.Titulo, "Titulo"); 
            var validarInformacoes = noticia.ValidaPropriedadeString(noticia.Informacao, "Informacoes"); 
            if(validarTitulo && validarInformacoes)
            {
                noticia.DataAlteracao = DateTime.Now;
                noticia.DataCadastro = DateTime.Now;
                noticia.Ativo = true;
                await _noticia.Update(noticia);
            }
        }

        public async Task DeleteNoticia(Noticia noticia)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Noticia>> GetAllAtive()
        {
            return await _noticia.GetAllNoticia(n => n.Ativo);
        }
    }
}
