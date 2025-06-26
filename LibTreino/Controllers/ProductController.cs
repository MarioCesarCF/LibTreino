using LibTreino.Enums;
using LibTreino.Models;
using LibTreino.Models.ViewModels.Produto;
using LibTreino.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibTreino.Controllers
{
    // Estudar melhor como fazer o controller e colocar mensagens de status code e erros
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _produtoService;

        public ProductController(ProductService produtoService)
        {
            _produtoService = produtoService;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<Product>> GetProdutosAsync()
        {
            return await _produtoService.GetAsync();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<Product> RetornaProdutoAsync(string id)
        {
            return await _produtoService.GetAsync(id);
        }

        [Authorize]
        [HttpPost]        
        public async Task<Product> CreateProdutoAsync(CreateProduct novoProduto)
        {
            var produto = await _produtoService.CreateAsync(novoProduto);

            return produto;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task UpdateProdutoAsync(string id, UpdateProduct produtoAlterado)
        {
            await _produtoService.UpdateAsync(id, produtoAlterado);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task RemoveProdutoAsync(string id)
        {
            await _produtoService.RemoveAsync(id);
        }
    }
}
