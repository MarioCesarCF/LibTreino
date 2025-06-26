using System.Text.Json.Serialization;

namespace LibTreino.Models.ViewModels.Usuario
{
    public class CreateUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; }       
        public IEnumerable<ShoppingList>? Listas { get; set; }
    }
}
