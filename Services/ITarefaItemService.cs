using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tarefas.Models;

namespace Tarefas.Services
{
    public interface ITarefaItemService
    {
        Task<IEnumerable<TarefaItem>> GetItensAsync(); 

        Task<TarefaItem> GetItemAsync(Guid? id);          

        Task<bool> AdicionarItem(TarefaItem tarefaItem);

        Task<bool> EditarItem(TarefaItem tarefaItem);

        Task<bool> DeletarItem(Guid? id);


    }
}