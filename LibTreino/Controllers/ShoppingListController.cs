using LibTreino.Models.ViewModels.Produto;
using LibTreino.Models;
using LibTreino.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibTreino.Models.ViewModels.Lista;

namespace LibTreino.Controllers
{
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private readonly ShoppingListService _shoppingListService;

        public ShoppingListController(ShoppingListService shoppingListService)
        {
            _shoppingListService = shoppingListService;
        }

        [HttpGet]
        [Route("api/shoppinglist")]
        public async Task<List<ShoppingList>> GetProdutosAsync()
        {
            return await _shoppingListService.GetAsync();
        }

        [HttpGet]
        [Route("api/shoppinglist/{id}")]
        public async Task<ShoppingList> RetornaProdutoAsync(string id)
        {
            return await _shoppingListService.GetAsync(id);
        }

        [HttpPost]
        [Route("api/shoppinglist")]
        public async Task<ShoppingList> CreateProdutoAsync(CreateShoppingList newShoppingList)
        {
            var shoppingList = await _shoppingListService.CreateAsync(newShoppingList);

            return shoppingList;
        }

        [HttpPut]
        [Route("api/shoppinglist/{id}")]
        public async Task UpdateProdutoAsync(string id, UpdateShoppingList updateShoppingList)
        {
            await _shoppingListService.UpdateAsync(id, updateShoppingList);
        }

        [HttpDelete]
        [Route("api/shoppinglist/{id}")]
        public async Task RemoveProdutoAsync(string id)
        {
            await _shoppingListService.RemoveAsync(id);
        }
    }
}
