using API.Application.Interfaces;
using API.Application.Token;
using API.Domain.Interfaces;
using API.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Application.Applications
{
    public class ApplicationUsuario : IApplicationUsuario
    {
        private readonly IUsuario _usuario;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _userSignIn;
        public ApplicationUsuario(IUsuario usuario, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> userSignIn)
        {
            _usuario = usuario;
            _userManager = userManager;
            _userSignIn = userSignIn;
        }

        public async Task<bool> AddUsuario(string email, string senha, int idade, string celular)
        {
            return await _usuario.AddUsuario(email,senha,idade,celular);
        }
        public async Task<string> GerarToken(string email, string senha)
        {
            if(string.IsNullOrWhiteSpace(email) || 
            string.IsNullOrWhiteSpace(senha))
            {
                return string.Empty;
            }
            var result = await _userSignIn.PasswordSignInAsync(email,senha,false,lockoutOnFailure: false);
            
            if(result.Succeeded)
            {
                var IdUsuario = await RetornoIdUsuario(email);

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
