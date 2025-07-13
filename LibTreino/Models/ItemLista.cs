using LibTreino.Enums;

namespace LibTreino.Models
{
    public class ItemLista
    {
        public string ProdutoId { get; set; }
        public string? Nome { get; set; }
        public int Quantidade { get; set; }
        public int Unidade { get; set; }
        public int Situacao { get; set; }
    }
}
