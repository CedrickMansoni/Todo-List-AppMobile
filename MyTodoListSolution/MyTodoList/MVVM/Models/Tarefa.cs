

using System.ComponentModel.DataAnnotations;

namespace MyTodoList.MVVM.Models
{
    public class Tarefa
    {
        [Key]
        public string IdTask { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public bool Estado {  get; set; }
    }
}
