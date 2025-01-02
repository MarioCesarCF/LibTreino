namespace LibTreino.Models.ViewModels.Lista
{
    public class UpdateShoppingList
    {
        public string? Title { get; set; }
        public IEnumerable<Models.Product>? Products { get; set; }
    }
}
