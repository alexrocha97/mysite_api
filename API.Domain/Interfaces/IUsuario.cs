using API.Domain.Models;

namespace API.Domain.Interfaces
{
    public interface IUsuario
    {
        Task<bool> AddUsuario(string email, string senha, int idade, string celular);
        Task<bool> IsExistsUser(string email, string senha);
        Task<string> RetornoIdUsuario(string email);
        Task<IEnumerable<Usuario>> ListaUsuario();
    }
}
