namespace API.Domain.Interfaces
{
    public interface IUsuario
    {
        Task<bool> AddUsuario(string email, string senha, int idade, string celular);
    }
}
