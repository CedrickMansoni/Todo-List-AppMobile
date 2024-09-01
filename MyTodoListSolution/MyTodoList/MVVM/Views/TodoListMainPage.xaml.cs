
using MyTodoList.MVVM.Models;
using MyTodoList.MVVM.ViewModels;
using MyTodoList.Repository.Services;

namespace MyTodoList.MVVM.Views;

public partial class TodoListMainPage : ContentPage
{
    public TodoListMainPage(ITarefaRepository contexto)
	{
		InitializeComponent();
		this.BindingContext = new MVVM.ViewModels.TarefaViewModel(contexto);
	}
}