using Microsoft.EntityFrameworkCore;
using standby_data.Context;
using standby_data.Interfaces;

namespace standby_data.Repositories
{
    public class RepositoryBase<T> : IRepositoryModel<T>, IDisposable where T : class
    {
        protected standby_orgContext context;
        public bool saveChanges = true;

        public RepositoryBase(bool _saveChanges = true)
        {
            saveChanges = _saveChanges;
            context = new standby_orgContext();
        }

        public T Adicionar(T entity)
        {
            context.Set<T>().Add(entity);
            if (saveChanges)
            {
                context.SaveChanges();
            }

            return entity;
        }

        public T Atualizar(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            if (saveChanges)
            {
                context.SaveChanges();
            }

            return entity;
        }

        public T BuscarPorID(int id)
        {
            return context.Set<T>().Find(id);
        }

        public List<T> BuscarTodos()
        {
            return context.Set<T>().ToList();
        }

        public void Deletar(T entity)
        {
            context.Set<T>().Remove(entity);
            if (saveChanges)
            {
                context.SaveChanges();
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void SalvarModificacoes()
        {
            context.SaveChanges();
        }
    }
}