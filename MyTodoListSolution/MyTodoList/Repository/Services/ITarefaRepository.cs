
using MyTodoList.MVVM.Models;

namespace MyTodoList.Repository.Services
{
    public interface ITarefaRepository
    {
        Task<Tarefa?> AddTarefa(Tarefa tarefa);
        Task<string> DeleteTarefa(string id);
        Task<Tarefa?> GetTarefa(string id);
        Task<List<Tarefa>> GetTarefas();
        Task<List<Tarefa>> GetTarefas(bool estado);
        Task<bool> PutTarefa(Tarefa tarefa);
    }
}
