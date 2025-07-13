using LibTreino.Enums;

namespace LibTreino.Models.DTOs
{
    public class ItemListaDto
    {
        public string ProdutotId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public int Unidade { get; set; }
        public int Situacao { get; set; }
    }
}
