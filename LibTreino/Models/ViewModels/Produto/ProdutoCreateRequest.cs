using LibTreino.Enums;

namespace LibTreino.Models.ViewModels.Produto
{
    public class ProdutoCreateRequest
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public Unity Unity { get; set; }
    }
}
