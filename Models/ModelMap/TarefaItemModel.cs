using Microsoft.EntityFrameworkCore;

namespace Tarefas.Models.ModelMap
{
    public class TarefaItemModel
    {
        public TarefaItemModel (){ }   

        public ModelBuilder ModelCreating(ModelBuilder builder)
        {
            builder.Entity<TarefaItem>().HasKey(k => k.Id);
            return builder;
        }
    }
}