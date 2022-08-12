using API.Application.Interfaces;
using API.Application.Token;
using API.Domain.Interfaces;
using API.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Application.Services
{
    public class WebTokenManager : IWebTokenManager
    {
        private readonly IUsuario _usuario;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _userSignIn;
        private readonly IWebTokenManager _webTokenManager;
        public WebTokenManager(
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
            var result = await _userSignIn.PasswordSignInAsync(email,senha,false,lockoutOnFailure: false);
            if(result.Succeeded)
            {
                var token = new WebTokenBuild()
                    .AddSecurityKey(WebTokenSecurity
                        .Create("Secret_Key-12345678"))
                        .AddSubject("Empresa - Canal de Noticia")
                        .AddIssuer("Teste.Securiry.Bearer")
                        .AddAudience("Teste.Securiry.Bearer")
                        .AddClaim("IdUsuario", IdUsuario)
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
