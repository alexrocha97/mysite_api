namespace API.Application.Interfaces
{
    public interface IWebTokenManager
    {
        bool DadosValidos(string email, string senha);
        Task<string> TokenUser(string email, string senha, string IdUsuario);
        Task<string> Create(string email, string senha, string celular);
    }
}