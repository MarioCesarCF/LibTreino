namespace LibTreino.Models.ViewModels.Lista
{
    public class CreateShoppingList
    {
        public string Title { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
