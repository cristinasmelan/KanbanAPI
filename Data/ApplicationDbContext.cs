using Microsoft.EntityFrameworkCore;
using KanbanAPI.Models;
using System.Collections.Generic;

namespace KanbanAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}