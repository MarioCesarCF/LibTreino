using LibTreino.Enums;
using System.Text.Json.Serialization;

namespace LibTreino.Models.ViewModels.Lista
{
    public class CreateShoppingList
    {
        public string Title { get; set; }
        public IEnumerable<Models.Product> Products { get; set; }
    }
}
