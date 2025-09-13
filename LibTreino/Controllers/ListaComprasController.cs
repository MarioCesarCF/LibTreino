using LibTreino.Enums;
using LibTreino.Models;
using LibTreino.Models.DTOs;
using LibTreino.Models.ViewModels.Lista;
using LibTreino.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace LibTreino.Controllers
{
    [ApiController]
    [Route("api/listacompras")]
    public class ListaComprasController : ControllerBase
    {
        private readonly ListaComprasService _shoppingListService;

        public ListaComprasController(ListaComprasService shoppingListService)
        {
            _shoppingListService = shoppingListService;
        }
        
        [Authorize]
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
                    Id = prod.Id,
                    Nome = prod.Nome,
                    Quantidade = prod.Quantidade,
                    Unidade = ToEnumDTO<Unidade>(prod.Unidade),
                    Situacao = prod.Situacao
                }).ToList().OrderBy(x => x.Nome)
            }).ToList();

            return dtoListas;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ListaCompras> RetornaShoppingListAsync(string id)
        {
            return await _shoppingListService.GetAsync(id);
        }

        [Authorize]
        [HttpPost]
        public async Task<ListaCompras> CreateShoppingListAsync(CriaListaCompras newShoppingList)
        {
            var shoppingList = await _shoppingListService.CreateAsync(newShoppingList);

            return shoppingList;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShoppingListAsync([FromBody] AtualizaListaCompras updateShoppingList)
        {
            await _shoppingListService.UpdateAsync(updateShoppingList);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveShoppingListAsync(string id)
        {
            await _shoppingListService.RemoveAsync(id);
            return NoContent();
        }

        [Authorize]
        [HttpPost("add-item")]
        public async Task<IActionResult> AddProductToListAsync([FromBody] AdicionaProdutoNaLista dto)
        {
            await _shoppingListService.AddProductAsync(dto);
            return NoContent();
        }

        [Authorize]
        [HttpPost("remove-item")]
        public async Task<IActionResult> RemoveProductToListAsync([FromBody] RemoveProdutoLista dto)
        {
            await _shoppingListService.RemoveProductAsync(dto);
            return NoContent();
        }

        [Authorize]
        [HttpPost("edit-item")]
        public async Task<IActionResult> UpdateItemInListAsync([FromBody] AtualizaItemListaDto itemAtualizado)
        {
            var lista = await _shoppingListService.GetAsync(itemAtualizado.ListaComprasId);
            if (lista == null)
                throw new Exception("Lista de compras não encontrada.");

            var item = lista.ItemLista.FirstOrDefault(i => i.Id == itemAtualizado.Id);
            if (item == null)
                throw new Exception("Item da lista não encontrado.");
                        
            item.Nome = itemAtualizado.Nome;
            item.Quantidade = itemAtualizado.Quantidade;
            item.Unidade = itemAtualizado.Unidade;
            item.Situacao = itemAtualizado.Situacao;

            lista.ItemLista.ToList().Add(item);

            var listaAtualizada = new AtualizaListaCompras
            {
                Id = lista.Id,
                ItemLista = lista.ItemLista
            };

            await _shoppingListService.UpdateAsync(listaAtualizada);
            return NoContent();
        }

        private EnumDTO ToEnumDTO<TEnum>(int valor) where TEnum : Enum
        {
            if (!Enum.IsDefined(typeof(TEnum), valor))
            {
                return new EnumDTO
                {
                    Id = null,
                    Descricao = "Desconhecido",
                    Valor = valor
                };
            }

            var enumValue = (TEnum)Enum.ToObject(typeof(TEnum), valor);

            return new EnumDTO
            {
                Id = enumValue.ToString(),
                Descricao = ((Enum)(object)enumValue).GetDisplayName(),
                Valor = valor
            };
        }
    }
}
