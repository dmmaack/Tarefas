using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tarefas.Data;
using Tarefas.Models;

namespace Tarefas.Services
{
    public class TempTarefaItemService : ITarefaItemService
    {
        private readonly ApplicationDbContext _context;

        public TempTarefaItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AdicionarItem(TarefaItem tarefaItem)
        {
            this._context.Add(tarefaItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditarItem(TarefaItem tarefaItem)
        {
            try
            {
                _context.Update(tarefaItem);
                int retorno = await _context.SaveChangesAsync();

                if (retorno > 0)
                    return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ContatoExiste(tarefaItem.Id))
                    throw new Exception("Tarefa não Existe!");
                else
                    throw ex;
            }

            return false;
        }

        private bool ContatoExiste(Guid id)
        {
            return _context.Tarefas.Any(a => a.Id == id);
        }

        public async Task<TarefaItem> GetItemAsync(Guid? id)
        {
            if (null == id)
                throw new Exception("Tarefa não Existe!");

            var tarefa = await _context.Tarefas.SingleOrDefaultAsync(s => s.Id == id);
            return tarefa;
        }

        public async Task<IEnumerable<TarefaItem>> GetItensAsync()
        {
            return await _context.Tarefas
                                 .Where(t => t.EstaCompleta == false)
                                 .ToArrayAsync();
        }

        public async Task<bool> DeletarItem(Guid? id)
        {
            var tarefa = await GetItemAsync(id);

            if (tarefa == null)
                throw new Exception("Tarefa não Existe!");

            _context.Tarefas.Remove(tarefa);
            var retorno = await _context.SaveChangesAsync();

            return retorno > 0 ? true : false;
        }
    }
}