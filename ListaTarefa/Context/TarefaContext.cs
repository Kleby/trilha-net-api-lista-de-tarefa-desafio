using ListaTarefa.Entities;
using Microsoft.EntityFrameworkCore;

namespace ListaTarefa.Context
{
    public class TarefaContext: DbContext
    {

        public TarefaContext(DbContextOptions<TarefaContext> options) : base(options) { }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
