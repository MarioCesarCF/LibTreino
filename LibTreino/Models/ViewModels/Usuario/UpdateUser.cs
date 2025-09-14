namespace LibTreino.Models.ViewModels.Usuario
{
    public class UpdateUser
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public IEnumerable<ListaCompras>? Listas { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
