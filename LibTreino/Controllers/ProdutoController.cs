using LibTreino.Enums;
using LibTreino.Models;
using LibTreino.Models.DTOs;
using LibTreino.Models.ViewModels.Produto;
using LibTreino.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibTreino.Controllers
{
    // Estudar melhor como fazer o controller e colocar mensagens de status code e erros
    [ApiController]
    [Route("api/produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<List<Produto>> GetProdutosAsync()
        {
            return await _produtoService.GetAsync();
        }

        //[Authorize]
        [HttpGet]
        public async Task<List<Produto>> GetProdutoByNomeAsync(string nome)
        {
            return await _produtoService.GetByNome(nome);
        }

        //[Authorize]
        [HttpGet("{id}")]
        public async Task<Produto> RetornaProdutoAsync(string id)
        {
            return await _produtoService.GetAsync(id);
        }

        //[Authorize]
        [HttpPost]        
        public async Task<Produto> CreateProdutoAsync([FromBody] CriaProduto novoProduto)
        {
            var produto = await _produtoService.CreateAsync(novoProduto);

            return produto;
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task UpdateProdutoAsync(string id, AtualizaProduto produtoAlterado)
        {
            await _produtoService.UpdateAsync(id, produtoAlterado);
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task RemoveProdutoAsync(string id)
        {
            await _produtoService.RemoveAsync(id);
        }

        //[Authorize]
        [HttpGet, Route("unidades")]
        public async Task<List<EnumDTO>> GetUnidadesAsync()
        {
            return await Task.Run(() => EnumDTO.ToList<Unidade>());
        }
    }
}
