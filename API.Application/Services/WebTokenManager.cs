using System.Text;
using API.Application.Interfaces;
using API.Application.Token;
using API.Domain.Enums;
using API.Domain.Interfaces;
using API.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace API.Application.Services
{
    public class WebTokenManager : IWebTokenManager
    {
        private readonly IUsuario _usuario;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _userSignIn;
        public WebTokenManager(
            IUsuario usuario, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> userSignIn)
        {
            _usuario = usuario;
            _userManager = userManager;
            _userSignIn = userSignIn;
        }

        public async Task<string> Create(string email, string senha, string celular)
        {
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

        public bool DadosValidos(string email, string senha)
        {
            if(string.IsNullOrWhiteSpace(email) || 
            string.IsNullOrWhiteSpace(senha))
            {
                return false;
            }
            return true;
        }

        public async Task<string> TokenUser(string email, string senha, string IdUsuario)
        {
            var Id = IdUsuario;
            var result = await _userSignIn.PasswordSignInAsync(email,senha,false,lockoutOnFailure: false);
            if(result.Succeeded)
            {
                var token = new WebTokenBuild()
                    .AddSecurityKey(WebTokenSecurity
                        .Create("Secret_Key-12345678"))
                        .AddSubject("Empresa - Canal de Noticia")
                        .AddIssuer("Teste.Securiry.Bearer")
                        .AddAudience("Teste.Securiry.Bearer")
                        .AddClaim("IdUsuario", Id)
                        .AddExpiry(60)
                        .Builder();
                
                return token.value;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
