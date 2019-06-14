using System;
using System.Collections.Generic;
using System.Linq;

namespace Tarefas.Data.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : class
	{
		TEntity BuscarPorId(int id);

        IEnumerable<TEntity> Buscar(Func<TEntity, bool> predicate);

		IEnumerable<TEntity> BuscarTodos();

		void Adicionar(TEntity entity);

		void Atualizar(TEntity entity);
			
		void Deletar(int id);

		void Deletar(Func<TEntity, bool> predicate);

		void Commit();

		void Dispose();
	}
}