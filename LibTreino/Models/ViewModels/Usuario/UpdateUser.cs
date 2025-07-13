namespace LibTreino.Models.ViewModels.Usuario
{
    public class UpdateUser
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public IEnumerable<Models.ListaCompras>? Listas { get; set; }
    }
}
