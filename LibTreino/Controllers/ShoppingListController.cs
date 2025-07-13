using LibTreino.Enums;
using LibTreino.Models;
using LibTreino.Models.Commons;
using LibTreino.Models.DTOs;
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
        public async Task<List<ShoppingListDTO>> GetShoppingListsAsync()
        {
            var listas = await _shoppingListService.GetAsync();

            var dtoListas = listas.Select(lista => new ShoppingListDTO
            {
                Id = lista.Id,
                Title = lista.Title,
                Products = lista.Products.Select(prod => new ProductDTO
                {
                    Id = prod.Id,
                    Name = prod.Name,
                    Amount = prod.Amount,
                    Unity = prod.Unity.ToEnumDto(),
                    Situation = prod.Situation.ToEnumDto()
                }).ToList()
            }).ToList();

            return dtoListas;
        }

        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ShoppingList> RetornaShoppingListAsync(string id)
        {
            return await _shoppingListService.GetAsync(id);
        }

        //[Authorize]
        [HttpPost]
        public async Task<ShoppingList> CreateShoppingListAsync(CreateShoppingList newShoppingList)
        {
            var shoppingList = await _shoppingListService.CreateAsync(newShoppingList);

            return shoppingList;
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShoppingListAsync([FromBody] UpdateShoppingList updateShoppingList)
        {
            await _shoppingListService.UpdateAsync(updateShoppingList);
            return NoContent();
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveShoppingListAsync(string id)
        {
            await _shoppingListService.RemoveAsync(id);
            return NoContent();
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProductToListAsync([FromBody] AddProductToList dto)
        {
            await _shoppingListService.AddProductAsync(dto);
            return NoContent();
        }
    }
}
