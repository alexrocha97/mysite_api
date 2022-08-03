using API.Domain.Interfaces;
using API.Domain.Models;
using API.Infra.Configuration;
using Microsoft.EntityFrameworkCore;

namespace API.Infra.Repository.Generics
{
    public class RepositoryUsuario : RepositoryGenerics<ApplicationUser>, IUsuario
    {
        private readonly DbContextOptions<Contexto> _optionsBuilder;
        public RepositoryUsuario()
        {
            _optionsBuilder = new DbContextOptions<Contexto>();
        }
        public async Task<bool> AddUsuario(string email, string senha, int idade, string celular)
        {
            try
            {
                using(var data = new Contexto(_optionsBuilder))
                {
                    await data.ApplicationUser.AddAsync(
                        new ApplicationUser
                        {
                            Email = email,
                            PasswordHash = senha,
                            Idade = idade,
                            Celular = celular
                        });

                    await data.SaveChangesAsync();
                }
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }
    }
}
