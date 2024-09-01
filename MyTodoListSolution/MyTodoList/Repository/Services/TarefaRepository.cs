
using Microsoft.EntityFrameworkCore;
using MyTodoList.AppContextDb;
using MyTodoList.MVVM.Models;

namespace MyTodoList.Repository.Services
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext _contextdb;
        public TarefaRepository(AppDbContext contextdb)
        {
            _contextdb = contextdb;
        }

        public async Task<Tarefa?> AddTarefa(Tarefa tarefa)
        {
            tarefa.IdTask = Guid.NewGuid().ToString("N");

            await _contextdb.TabelaTarefa.AddAsync(tarefa);

            try
            {
                if (await _contextdb.SaveChangesAsync() == 1)
                {
                    return await _contextdb.TabelaTarefa.FirstOrDefaultAsync(u => u.IdTask == tarefa.IdTask);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", $"{ex}", "Ok");
            }
            return null;
        }

        public async Task<string> DeleteTarefa(string id)
        {
            var tarefa = await _contextdb.TabelaTarefa.FirstOrDefaultAsync(t => t.IdTask == id);
            if (tarefa is null)
                return "Nao existe nenhuma tarefa com este Id";

            _contextdb.TabelaTarefa.Remove(tarefa);
            return await _contextdb.SaveChangesAsync() == 1 ? "Tarefa eliminada com sucesso" : "Nao foi possivel eliminar a tarefa";
        }

        public async Task<Tarefa?> GetTarefa(string id)
        {
            return await _contextdb.TabelaTarefa.FirstOrDefaultAsync(t => t.IdTask == id);
        }

        public async Task<List<Tarefa>> GetTarefas()
        {
            return await _contextdb.TabelaTarefa.ToListAsync();
        }

        public async Task<List<Tarefa>> GetTarefas(bool estado)
        {
            return await _contextdb.TabelaTarefa.Where(x => x.Estado == estado).ToListAsync();
        }

        public async Task<bool> PutTarefa(Tarefa tarefa)
        {
            var task = await _contextdb.TabelaTarefa.FirstOrDefaultAsync(t => t.IdTask == tarefa.IdTask);
            if (task  is not null)
            {
                task.Estado = tarefa.Estado;
                _contextdb.TabelaTarefa.Update(task);
                if (await _contextdb.SaveChangesAsync() == 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
