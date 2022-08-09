using System.Text;
using API.Application.Interfaces;
using API.Domain.Enums;
using API.Domain.Models;
using API.Token;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class loginController : ControllerBase
    {
        private readonly IApplicationUsuario _appUsuario;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _userSignIn;
        public loginController(IApplicationUsuario appUsuario, 
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> userSignIn)
        {
            _userManager = userManager;
            _userSignIn = userSignIn;
            _appUsuario = appUsuario;
        }

        // [HttpPost("/api/CreateToken")]
        // [AllowAnonymous]
        // [Produces("application/json")]
        // public async Task<IActionResult> CreateToken([FromBody]Login login)
        // {
        //     if(string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
        //     {
        //         return Unauthorized();
        //     }
        //     var result = await _appUsuario.IsExistsUser(login.email, login.senha);
        //     if(result)
        //     {
                // var IdUsuario = await _appUsuario.RetornoIdUsuario(login.email);

        //         var token = new JsonWebTokenBuild()
        //             .AddSecurityKey(JsonWebTokenSecurity
        //                 .Create("Secret_Key-12345678"))
        //                 .AddSubject("Empresa - Canal de Noticia")
        //                 .AddIssuer("Teste.Securiry.Bearer")
        //                 .AddAudience("Teste.Securiry.Bearer").AddClaim("IdUsuario", IdUsuario)
        //                 .AddExpiry(5)
        //                 .Builder();
                
        //         return Ok(token.value);
        //     }
        //     else
        //     {
        //         return Unauthorized();
        //     }
        // }
        // [HttpPost("/api/CreateUsuario")]
        // [AllowAnonymous]
        // [Produces("application/json")]
        // public async Task<IActionResult> CreateUsuario([FromBody]Login login)
        // {
        //     if(string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
        //     {
        //         return Unauthorized();
        //     }
        //     var result = await 
        //         _appUsuario.AddUsuario(login.email, login.senha,login.idade,login.celular);
        //     if(result)
        //         return Ok("Criado o usuário com sucesso!");
        //     else
        //         return BadRequest("Erro ao criar usuário");
        // }

        // MESMA FORMA  DO ENDPOINT CREATETOKEN, MAS USANDO AS FERRAMENTAS DO IDENTITY
        [HttpPost("/api/CreateTokenIdentity")]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<IActionResult> CreateTokenIdentity([FromBody]Login login)
        {
            if(string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            {
                return Unauthorized();
            }
            var result = await _userSignIn.
                PasswordSignInAsync(login.email,login.senha,false,lockoutOnFailure: false);
            
            if(result.Succeeded)
            {
                var IdUsuario = await _appUsuario.RetornoIdUsuario(login.email);

                var token = new JsonWebTokenBuild()
                    .AddSecurityKey(JsonWebTokenSecurity
                        .Create("Secret_Key-12345678"))
                        .AddSubject("Empresa - Canal de Noticia")
                        .AddIssuer("Teste.Securiry.Bearer")
                        .AddAudience("Teste.Securiry.Bearer")
                        .AddClaim("IdUsuario", IdUsuario)
                        .AddExpiry(60)
                        .Builder();
                
                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("/api/CreateUserIdentity")]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<IActionResult> CreateUserIdentity([FromBody]Login login)
        {
            if(string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            {
                return Unauthorized();
            }
            var user = new ApplicationUser()
            {
                UserName = login.email,
                Email = login.email,
                Celular = login.celular,
                Tipo = TipoUsuario.Comum
            };
            var result = await _userManager.CreateAsync(user, login.senha);
            if(result.Errors.Any())
            {
                return Ok(result.Errors);
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
                return Ok("Usuário confirmado com sucesso...!");
            
            return BadRequest("Erro ao confirmar usuário.");

        }


    }
}
