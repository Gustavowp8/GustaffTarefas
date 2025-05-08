using GustaffTarefas.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GustaffTarefas.Data
{
    public class Context : IdentityDbContext
    {
        public Context(DbContextOptions<Context>options) :base(options) { }
        public DbSet<TarefaModel> Tarefas { get; set; }
    }
}
