namespace LibTreino.Models.ViewModels.Usuario
{
    public class UpdateUser
    {
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Senha { get; set; }
        public IEnumerable<Models.ShoppingList>? Listas { get; set; }
    }
}
