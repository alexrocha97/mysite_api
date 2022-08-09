namespace API.Application.Interfaces
{
    public interface IApplicationUsuario
    {
        Task<bool> AddUsuario(string email, string senha, int idade, string celular);
        Task<bool> IsExistsUser(string email, string senha);
        Task<string> RetornoIdUsuario(string email);
    }
}
