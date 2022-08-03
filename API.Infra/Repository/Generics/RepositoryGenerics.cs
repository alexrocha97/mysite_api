using System.Runtime.InteropServices;
using API.Domain.Interfaces.Generics;
using API.Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;

namespace API.Infra.Repository.Generics
{
    public class RepositoryGenerics<TEntity> : IGenerics<TEntity>, IDisposable where TEntity : class
    {
        private readonly DbContextOptions<Contexto> _optionsBuilder;
        public RepositoryGenerics()
        {
            _optionsBuilder = new DbContextOptions<Contexto>();
        }
        public async Task Add(TEntity obj)
        {
            using(var data = new Contexto(_optionsBuilder))
            {
                await data.Set<TEntity>().AddAsync(obj);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(TEntity obj)
        {
             using(var data = new Contexto(_optionsBuilder))
            {
                data.Set<TEntity>().Remove(obj);
                await data.SaveChangesAsync();
            }
        }

        public async Task<List<TEntity>> GetAll()
        {
            using(var data = new Contexto(_optionsBuilder))
            {
                return await data.Set<TEntity>().AsNoTracking().ToListAsync();
            }
        }

        public async Task<TEntity> GetById(int Id)
        {
            using(var data = new Contexto(_optionsBuilder))
            {
                return await data.Set<TEntity>().FindAsync(Id);
            }
        }

        public async Task Update(TEntity obj)
        {
            using(var data = new Contexto(_optionsBuilder))
            {
                data.Set<TEntity>().Update(obj);
                await data.SaveChangesAsync();
            }
        }

        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);


        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
        #endregion
    }
}
