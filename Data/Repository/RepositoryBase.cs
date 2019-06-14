using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Tarefas.Data;

namespace Tarefas.Data.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>, IDisposable where TEntity : class
    {
        internal ApplicationDbContext Context;

        public RepositoryBase(ApplicationDbContext context)
        {
            this.Context = context;
        }

        public virtual TEntity BuscarPorId(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> Buscar(Func<TEntity, bool> predicate)
        {
            return Context.Set<TEntity>()
						  .Where(predicate);
        }

        public virtual IEnumerable<TEntity> BuscarTodos()
        {
            return Context.Set<TEntity>().AsEnumerable();
        }

		public virtual void Adicionar(TEntity entity)
		{
			Context.Set<TEntity>().Add(entity);
		}

		public virtual void Atualizar(TEntity entity)
		{
			Context.Entry<TEntity>(entity).State = EntityState.Modified;
		}

        public virtual void Deletar(int id)
        {
            TEntity obj = BuscarPorId(id);
			Context.Set<TEntity>().Remove(obj);
        }

		public virtual void Deletar(Func<TEntity, bool> predicate)
		{
			Context.Set<TEntity>()
				.Where(predicate).ToList()
				.ForEach(del => Context.Set<TEntity>().Remove(del));
		}

		public void Commit()
		{
			Context.SaveChanges();
		}

		public void Dispose()
		{
			if (Context != null)
				Context.Dispose();
		}
    }
}