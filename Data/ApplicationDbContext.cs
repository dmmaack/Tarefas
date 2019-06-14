using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tarefas.Models;
using Tarefas.Models.ModelMap;

namespace Tarefas.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TarefaItem> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder = new TarefaItemModel().ModelCreating(builder);

            base.OnModelCreating(builder);
        }
    }
}
