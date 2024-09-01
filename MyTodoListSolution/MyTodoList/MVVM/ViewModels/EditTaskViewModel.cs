using MyTodoList.MVVM.Models;
using MyTodoList.Repository.Services;

namespace MyTodoList.MVVM.ViewModels
{
    public class EditTaskViewModel : BindableObject
    {
        private readonly ITarefaRepository _contexto;
        public EditTaskViewModel(ITarefaRepository contexto, Tarefa task)
        {
            _contexto = contexto;
            Tarefa = task;
        }

        private Tarefa tarefa;
        public Tarefa Tarefa
        {
            get { return tarefa; }
            set { tarefa = value; OnPropertyChanged(nameof(Tarefa)); }
        }
    }
}
