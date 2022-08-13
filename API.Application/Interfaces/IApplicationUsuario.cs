using API.Domain.Enums;
using API.Domain.Models;
using API.Domain.Notifications;

namespace API.Application.Interfaces
{
    public interface IApplicationUsuario
    {
        Task<bool> AddUsuario(string email, string senha, int idade, string celular);
        Task<Notifica> CreateUser(string email, string senha, string celular);
        Task<string> GerarToken(string email, string senha);
        Task<bool> IsExistsUser(string email, string senha);
        Task<string> RetornoIdUsuario(string email);
        Task<IEnumerable<Usuario>> ListaDeUsuario();
    }
}
