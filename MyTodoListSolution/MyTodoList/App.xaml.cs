using MyTodoList.MVVM.Views;
using MyTodoList.Repository.Services;

namespace MyTodoList
{
    public partial class App : Application
    {
        public App(ITarefaRepository contexto)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TodoListMainPage(contexto));
        }
    }
}
