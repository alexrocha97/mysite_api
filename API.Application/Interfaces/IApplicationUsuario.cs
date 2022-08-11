namespace API.Application.Interfaces
{
    public interface IApplicationUsuario
    {
        Task<bool> AddUsuario(string email, string senha, int idade, string celular);
        // Task<string> CreateUser(string email, string senha);
        Task<string> GerarToken(string email, string senha);
        Task<bool> IsExistsUser(string email, string senha);
        Task<string> RetornoIdUsuario(string email);
    }
}
