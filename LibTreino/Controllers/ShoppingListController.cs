using LibTreino.Models;
using LibTreino.Models.ViewModels.Lista;
using LibTreino.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibTreino.Controllers
{
    [ApiController]
    [Route("api/shoppinglist")]
    public class ShoppingListController : ControllerBase
    {
        private readonly ShoppingListService _shoppingListService;

        public ShoppingListController(ShoppingListService shoppingListService)
        {
            _shoppingListService = shoppingListService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<List<ShoppingList>> GetShoppingListsAsync()
        {
            return await _shoppingListService.GetAsync();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ShoppingList> RetornaShoppingListAsync(string id)
        {
            return await _shoppingListService.GetAsync(id);
        }

        [Authorize]
        [HttpPost]
        public async Task<ShoppingList> CreateShoppingListAsync(CreateShoppingList newShoppingList)
        {
            var shoppingList = await _shoppingListService.CreateAsync(newShoppingList);

            return shoppingList;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShoppingListAsync(string id, UpdateShoppingList updateShoppingList)
        {
            await _shoppingListService.UpdateAsync(id, updateShoppingList);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveShoppingListAsync(string id)
        {
            await _shoppingListService.RemoveAsync(id);
            return NoContent();
        }
    }
}
