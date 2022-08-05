using API.Application.Interfaces;
using API.Domain.Interfaces;

namespace API.Application.Applications
{
    public class ApplicationUsuario : IApplicationUsuario
    {
        private readonly IUsuario _usuario;
        public ApplicationUsuario(IUsuario usuario)
        {
            _usuario = usuario;
        }
        public async Task<bool> AddUsuario(string email, string senha, int idade, string celular)
        {
            return await _usuario.AddUsuario(email,senha,idade,celular);
        }

        public async Task<bool> IsExistsUser(string email, string senha)
        {
            return await _usuario.IsExistsUser(email,senha);
        }
    }
}
