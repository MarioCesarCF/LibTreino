namespace LibTreino.Models.ViewModels.Lista
{
    public class UpdateShoppingList
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
