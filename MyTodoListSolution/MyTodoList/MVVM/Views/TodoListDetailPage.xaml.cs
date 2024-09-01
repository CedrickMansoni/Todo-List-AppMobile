using MyTodoList.MVVM.Models;
using MyTodoList.Repository.Services;

namespace MyTodoList.MVVM.Views;

public partial class TodoListDetailPage : ContentPage
{
	public TodoListDetailPage(ITarefaRepository contexto, Tarefa task)
	{
		InitializeComponent();
        this.BindingContext = new MVVM.ViewModels.EditTaskViewModel(contexto, task);
    }
}