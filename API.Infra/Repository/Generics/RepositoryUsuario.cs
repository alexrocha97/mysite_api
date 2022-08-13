using API.Domain.Enums;
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

        public async Task<bool> IsExistsUser(string email, string senha)
        {
            try
            {
                using(var data = new Contexto(_optionsBuilder))
                {
                    return await data.ApplicationUser.
                        Where(x => 
                            x.Email.Equals(email) && 
                            x.PasswordHash.Equals(senha))
                            .AsNoTracking()
                            .AnyAsync();
                }
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<Usuario>> ListaUsuario()
        {
            using(var banco = new Contexto(_optionsBuilder))
            {
                var lstUsuario = new List<Usuario>();
                var usuarios = await banco.ApplicationUser.ToListAsync();
                foreach(var usuario in usuarios)
                {
                    var user = new Usuario()
                    {
                        Email = usuario.Email,
                        Celular = usuario.Celular,
                        Idade = usuario.Idade,
                        Tipo = Enum.GetName(typeof(TipoUsuario),usuario.Tipo)
                    };
                    lstUsuario.Add(user);
                }

                return lstUsuario;
            }
        }

        public async Task<string> RetornoIdUsuario(string email)
        {
             try
            {
                using(var data = new Contexto(_optionsBuilder))
                {
                    var usuario = await data.ApplicationUser.
                        Where(x => 
                            x.Email.Equals(email))
                            .AsNoTracking()
                            .FirstOrDefaultAsync();
                    
                    return usuario.Id;
                }
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }
    }
}
