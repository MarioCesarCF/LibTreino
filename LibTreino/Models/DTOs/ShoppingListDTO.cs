namespace LibTreino.Models.DTOs
{
    public class ShoppingListDTO
    {
        public string? Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
    }
}
