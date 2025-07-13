namespace LibTreino.Models.DTOs
{
    public class ListaComprasDto
    {
        public string? Id { get; set; }
        public string? Nome { get; set; }
        public IEnumerable<ItemListaDto>? ItemLista { get; set; }
    }
}
