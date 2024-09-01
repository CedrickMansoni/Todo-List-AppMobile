using Microsoft.Maui.Controls;
using MyTodoList.AppContextDb;
using MyTodoList.MVVM.Models;
using MyTodoList.Repository.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyTodoList.MVVM.ViewModels
{
    public class TarefaViewModel : BindableObject
    {
        private readonly ITarefaRepository _contexto;
        public TarefaViewModel(ITarefaRepository contexto)
        {
            _contexto = contexto;
            _ = Carregar();
        }

        private async Task Carregar()
        {
            TarefaList = new ObservableCollection<Tarefa>(await _contexto.GetTarefas());
        }


        private string add = "Add";
        public string Add
        {
            get { return add; }
            set { add = value; OnPropertyChanged(nameof(Add)); }
        }

        private string tarefa;
        public string Tarefa 
        {
            get { return tarefa; } 
            set {  tarefa = value; OnPropertyChanged(nameof(Tarefa)); }
        }

        private ObservableCollection<Tarefa> tarefaList = new ObservableCollection<Tarefa>();
        public ObservableCollection<Tarefa> TarefaList { get { return tarefaList; } set { tarefaList = value; OnPropertyChanged(nameof(TarefaList)); } }

        public ICommand AddTarefaCommand => new Command(async() =>
        {
            if (string.IsNullOrEmpty(Tarefa))
            {
                await App.Current.MainPage.DisplayAlert("Erro", "Digite a tarefa", "Ok");
            }
            else
            {
                var task = new Tarefa() { Nome = Tarefa};
                var ntask = await _contexto.AddTarefa(task);
                if (ntask is not null)
                {
                    TarefaList.Add(ntask);
                    Tarefa = string.Empty;
                }
            }
        });

        public ICommand EditarEstadoCommand => new Command<Tarefa>(async(Tarefa tarefa) =>
        {
            tarefa.Estado = !tarefa.Estado;
            if(await _contexto.PutTarefa(tarefa))
            {
                await Carregar();
            }            
        });

        public ICommand DropDataBaseCommand => new Command(async () => 
        {
            var resposta = await App.Current.MainPage.DisplayAlert("Alerta","Deseja eliminar o banco de dados?","Sim","Nao");
            switch (resposta)
            {
                case true:
                    var dataBase = new AppDbContext();
                    await dataBase.Database.EnsureDeletedAsync();
                    await dataBase.Database.EnsureCreatedAsync();
                    await App.Current.MainPage.DisplayAlert("Mensagem", "Banco de dados eliminado com sucesso", "Ok");
                    await Carregar();
                    break;
                    case false: break;                        
            }           
        });

        public ICommand CarregarTarefasCommand => new Command(async () => 
        {
            await Carregar();
        });

        public ICommand LimparTelaCommand => new Command(async () =>
        {
            TarefaList.Clear();
        });

        public ICommand CarregarTaskCompletedCommand => new Command(async () => 
        {
            TarefaList = new ObservableCollection<Tarefa>(await _contexto.GetTarefas(true));
        });

        public ICommand CarregarTaskNoCompletedCommand => new Command(async () =>
        {
            TarefaList = new ObservableCollection<Tarefa>(await _contexto.GetTarefas(false));
        });

        private Tarefa? editarTask;
        public Tarefa? EditarTask
        {
            get { return editarTask; }
            set { editarTask = value; OnPropertyChanged(nameof(EditarTask)); }
        }
        public ICommand EditTaskCommand => new Command<Tarefa>((Tarefa task) =>
        {
            EditarTask = task;
            OptionTask();
        });
        public ICommand PutTaskCommand => new Command(async () =>
        {
            if (string.IsNullOrEmpty(EditarTask.Nome))
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Digite uma tarefa","Ok");
            }
            else
            {
                var response = await _contexto.PutTarefa(EditarTask);
                if (response)
                {
                    OptionTask();
                    var t = TarefaList.FirstOrDefault(t => t.IdTask == EditarTask.IdTask);
                    if (t != null) 
                    {
                        try
                        {
                            int index = TarefaList.IndexOf(t);
                            TarefaList.Remove(t);
                            TarefaList.Insert(index, EditarTask);
                        }
                        catch
                        {

                        }

                    }

                    EditarTask = null;
                }
            }
           
        });

        public ICommand DeleteTaskCommand => new Command<Tarefa>(async(Tarefa task) =>
        {
            var response = await _contexto.DeleteTarefa(task.IdTask);
            if (response.Contains("sucesso"))
            {
                TarefaList.Remove(task);
            }
        });


        private bool newTask = true;
        public bool NewTask
        {
            get { return newTask; }
            set { newTask = value; OnPropertyChanged(nameof(NewTask)); }
        }

        private bool editTask = false;
        public bool EditTask
        {
            get { return editTask; }
            set { editTask = value; OnPropertyChanged(nameof(EditTask)); }
        }

        private void OptionTask() 
        {
            NewTask = !NewTask; 
            EditTask = !EditTask;
        }
    }
}
