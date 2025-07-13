namespace LibTreino.Models.ViewModels.Lista
{
    public class AtualizaListaCompras
    {
        public string Id { get; set; }
        public string? Nome { get; set; }
        public IEnumerable<ItemLista>? ItemLista { get; set; }
    }
}
