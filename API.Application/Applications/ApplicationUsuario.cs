using System.Text;
using API.Application.Interfaces;
using API.Application.Token;
using API.Domain.Enums;
using API.Domain.Interfaces;
using API.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

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

        public async Task<bool> AddUsuario(string email, string senha, int idade, string celular)
        {
            return await _usuario.AddUsuario(email,senha,idade,celular);
        }

        public async Task<string> CreateUser(string email, string senha, string celular)
        {
            if(string.IsNullOrWhiteSpace(email) 
            || string.IsNullOrWhiteSpace(senha))
            {
                return string.Empty;
            }
            var user = new ApplicationUser()
            {
                UserName = email,
                Email = email,
                Celular = celular,
                Tipo = TipoUsuario.Comum
            };
            var result = await _userManager.CreateAsync(user, senha);
            if(result.Errors.Any())
            {
                return result.Errors.ToString();
            }
            // GERAÇÃO DE CONFIRMAÇÃO CASO PRECISE
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // RETORNO EMAIL DA CONFIRMAÇÃO
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultDeConfirmacao = await _userManager.ConfirmEmailAsync(user, code);

            var statusMessage = resultDeConfirmacao.Succeeded;

            if(resultDeConfirmacao.Succeeded)
                return "Usuário confirmado com sucesso...!";
            
            return "Erro ao confirmar usuário.";
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
