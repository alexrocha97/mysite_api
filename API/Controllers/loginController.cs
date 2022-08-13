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

        public loginController(IApplicationUsuario appUsuario)
        {
            _appUsuario = appUsuario;
        }

        #region Antigo endPoint
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
        #endregion

        [HttpGet("/api/ListaUsuarios")]
        [Authorize]
        [Produces("application/json")]
        public async Task<IEnumerable<Usuario>> ListaUsuarios()
        {
            return await _appUsuario.ListaDeUsuario();
        }


        // MESMA FORMA  DO ENDPOINT CREATETOKEN, MAS USANDO AS FERRAMENTAS DO IDENTITY
        [HttpPost("/api/CreateTokenIdentity")]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<IActionResult> CreateTokenIdentity([FromBody]LoginDto login)
        {
            try
            {
                var result = await _appUsuario.GerarToken(login.email,login.senha);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("/api/CreateUserIdentity")]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<IActionResult> CreateUserIdentity([FromBody]Login login)
        {
           try
           {
                var result = await _appUsuario.CreateUser(login.email,login.senha,login.celular);
                return Ok(result);
           }
           catch(Exception ex)
           {
                return BadRequest(ex);
           }

        }


    }
}
