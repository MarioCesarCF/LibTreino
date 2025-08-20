using LibTreino.Enums;

namespace LibTreino.Models.ViewModels.Lista
{
    public class AtualizaItemListaDto
    {
        public string ListaComprasId { get; set; }
        public string Id { get; set; }
        public string? Nome { get; set; }
        public int Quantidade { get; set; }
        public int Unidade { get; set; }
        public int Situacao { get; set; }
    }
}
