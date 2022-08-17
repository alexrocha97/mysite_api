using API.Application.Interfaces;
using API.Domain.Interfaces;
using API.Domain.Models;
using API.Domain.Notifications;
using Microsoft.AspNetCore.Identity;

namespace API.Application.Applications
{
    public class ApplicationUsuario : IApplicationUsuario
    {
        private readonly IUsuario _usuario;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _userSignIn;
        private readonly IWebTokenManager _webTokenManager;

        public ApplicationUsuario(
            IUsuario usuario, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> userSignIn, 
            IWebTokenManager webTokenManager)
        {
            _usuario = usuario;
            _userManager = userManager;
            _userSignIn = userSignIn;
            _webTokenManager = webTokenManager;
        }

        public async Task<IEnumerable<Usuario>> ListaDeUsuario()
        {
            return await _usuario.ListaUsuario();
        }

        public async Task<bool> AddUsuario(string email, string senha, int idade, string celular)
        {
            return await _usuario.AddUsuario(email,senha,idade,celular);
        }

        public async Task<Notifica> CreateUser(string email, string senha, string celular)
        {
            var isvalid = _webTokenManager.DadosValidos(email,senha);
            if(isvalid)
            {
                var result = await _webTokenManager.Create(email,senha,celular);
                return result;
            }
            var notificacao = new Notifica()
            {
                Mensagem = "Erro ao criar usuário!"
            };
            return notificacao;
        }

        public async Task<string> GerarToken(string email, string senha)
        {
            var isvalid = _webTokenManager.DadosValidos(email, senha);
            if(isvalid)
            {
                var IdUsuario = await _usuario.RetornoIdUsuario(email);
                var tokenString = await _webTokenManager.TokenUser(email, senha, IdUsuario);
                return tokenString;
            }
            return string.Empty;
        }

        public async Task<bool> IsExistsUser(string email, string senha)
        {
            return await _usuario.IsExistsUser(email,senha);
        }

        public async Task<string> RetornoIdUsuario(string email)
        {
            return await _usuario.RetornoIdUsuario(email);
        }
    }
}
