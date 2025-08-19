using LibTreino.Enums;

namespace LibTreino.Models.DTOs
{
    public class ItemListaDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public decimal Quantidade { get; set; }
        public EnumDTO Unidade { get; set; }
        public int Situacao { get; set; }
    }
}
