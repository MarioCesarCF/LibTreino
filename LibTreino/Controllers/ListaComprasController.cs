using LibTreino.Enums;
using LibTreino.Models;
using LibTreino.Models.Commons;
using LibTreino.Models.DTOs;
using LibTreino.Models.ViewModels.Lista;
using LibTreino.Models.ViewModels.Produto;
using LibTreino.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LibTreino.Controllers
{
    [ApiController]
    [Route("api/listacompras")]
    public class ListaComprasController : ControllerBase
    {
        private readonly ListaComprasService _shoppingListService;
        private readonly ProdutoService _produtoService;

        public ListaComprasController(ListaComprasService shoppingListService, ProdutoService produtoService)
        {
            _shoppingListService = shoppingListService;
            _produtoService = produtoService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<List<ListaComprasDto>> GetShoppingListsAsync()
        {
            var listas = await _shoppingListService.GetAsync();

            var dtoListas = listas.Select(lista => new ListaComprasDto
            {
                Id = lista.Id,
                Nome = lista.Nome,
                ItemLista = lista.ItemLista?.Select(prod => new ItemListaDto
                {
                    ProdutotId = prod.ProdutoId,
                    Nome = prod.Nome,
                    Quantidade = prod.Quantidade,
                    Unidade = prod.Unidade,
                    Situacao = prod.Situacao
                }).ToList() ?? new List<ItemListaDto>()
            }).ToList();

            return dtoListas;
        }

        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ListaCompras> RetornaShoppingListAsync(string id)
        {
            return await _shoppingListService.GetAsync(id);
        }

        //[Authorize]
        [HttpPost]
        public async Task<ListaCompras> CreateShoppingListAsync(CriaListaCompras newShoppingList)
        {
            var shoppingList = await _shoppingListService.CreateAsync(newShoppingList);

            return shoppingList;
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShoppingListAsync([FromBody] AtualizaListaCompras updateShoppingList)
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

        [HttpPost("add-item")]
        public async Task<IActionResult> AddProductToListAsync([FromBody] AdicionaProdutoNaLista dto)
        {
            var produtosPorNome = await _produtoService.GetByNome(dto.ItemLista.Nome);
            var itemAdicionado = new Produto();

            if (produtosPorNome.IsNullOrEmpty())
            {
                var criarProduto = new CriaProduto
                {
                    Nome = dto.ItemLista.Nome
                };

                itemAdicionado = await _produtoService.CreateAsync(criarProduto);
            }

            await _shoppingListService.AddProductAsync(dto);
            return NoContent();
        }

        [HttpPost("remove-item")]
        public async Task<IActionResult> RemoveProductToListAsync([FromBody] RemoveProdutoNaLista dto)
        {
            await _shoppingListService.RemoveProductAsync(dto);
            return NoContent();
        }
    }
}
