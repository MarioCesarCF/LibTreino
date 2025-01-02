using System.Text.Json.Serialization;

namespace LibTreino.Models.ViewModels.Usuario
{
    public class CreateUser
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? Telefone { get; set; }
        public string Senha { get; set; }       
        public IEnumerable<Models.ShoppingList>? Listas { get; set; }
    }
}
