using API.Application.Interfaces;
using API.Domain.Models;
using API.Domain.Notifications;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoticiaController : ControllerBase
    {
        private readonly IApplicationNoticia _appNoticia;
        public NoticiaController(IApplicationNoticia appNoticia)
        {
            _appNoticia = appNoticia;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/ListarNoticias")]
        public async Task<IEnumerable<Noticia>> ListarNoticias()
        {
            return await _appNoticia.GetAllAtive();
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/AdicionarNoticia")]
        public async Task<List<Notifica>> AdicionarNoticia(NoticiaMViewModel noticia)
        {
            var novaNoticia = new Noticia();
            novaNoticia.Titulo = noticia.Titulo;
            novaNoticia.Informacao = noticia.Informacao;
            novaNoticia.UserId = await RetornarIdUsuarioLogado();
            await _appNoticia.AddNoticia(novaNoticia);

            return novaNoticia.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/AtualizarNoticia")]
        public async Task<List<Notifica>> AtualizarNoticia(NoticiaMViewModel noticia)
        {
            var novaNoticia = await _appNoticia.GetById(noticia.IdNoticia);
            novaNoticia.Titulo = noticia.Titulo;
            novaNoticia.Informacao = noticia.Informacao;
            novaNoticia.UserId = await RetornarIdUsuarioLogado();
            await _appNoticia.UpdateNoticia(novaNoticia);

            return novaNoticia.Notificacoes;
        }


        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/ExcluirNoticia")]
        public async Task<List<Notifica>> ExcluirNoticia(NoticiaMViewModel noticia)
        {
            var novaNoticia = await _appNoticia.GetById(noticia.IdNoticia);
            await _appNoticia.DeleteNoticia(novaNoticia);

            return novaNoticia.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/ExcluirNoticia")]
        public async Task<Noticia> BuscarPorIdNoticia(NoticiaMViewModel noticia)
        {
            var novaNoticia = await _appNoticia.GetById(noticia.IdNoticia);
            return novaNoticia;
        }

        private async Task<string> RetornarIdUsuarioLogado()
        {
            if(User != null)
            {
                var IdUsuario = User.FindFirst("IdUsuario");
                var result = await Task.Run(() => IdUsuario);
                return result.Value;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
