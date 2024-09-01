
using Microsoft.EntityFrameworkCore;
using MyTodoList.DatabasePath;
using MyTodoList.MVVM.Models;

namespace MyTodoList.AppContextDb
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tarefa> TabelaTarefa { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.
            UseSqlite($"Filename={PathDB.GetPath("todo.db")}");
        
    }
}
