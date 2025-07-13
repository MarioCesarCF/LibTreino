using LibTreino.Data;
using LibTreino.Models;
using LibTreino.Models.ViewModels.Produto;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibTreino.Services
{
    public class ProdutoService
    {
        //Configuração de conexão do service com o banco.
        //Procurar como fazer isso de forma melhor
        private readonly IMongoCollection<Produto> _productCollection;

        public ProdutoService(IOptions<ConfigDatabaseSettings> productDatabaseOptions) 
        {
            var mongoClient = new MongoClient(productDatabaseOptions.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(productDatabaseOptions.Value.DatabaseName);

            _productCollection = mongoDatabase.GetCollection<Produto>(productDatabaseOptions.Value.ProductCollectionName);
        }

        public async Task<List<Produto>> GetAsync()
        {
            return await _productCollection.Find(x => true).ToListAsync();
        }

        public async Task<List<Produto>> GetByNome(string nome)
        {
            return await _productCollection.Find(x => x.Nome.ToLower().Contains(nome.ToLower())).ToListAsync();
        }

        public async Task<Produto> GetAsync(string id)
        {
            return await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Produto> CreateAsync(CriaProduto novoProduto)
        {
            var produto = new Produto
            {
                Nome = novoProduto.Nome
            };

            await _productCollection.InsertOneAsync(produto);

            return produto;
        }

        public async Task UpdateAsync(string id, AtualizaProduto produtoAlterado)
        {
            var updateDefinitions = new List<UpdateDefinition<Produto>>();

            if (!string.IsNullOrEmpty(produtoAlterado.Nome))
                updateDefinitions.Add(Builders<Produto>.Update.Set(p => p.Nome, produtoAlterado.Nome));

            var combinedUpdates = Builders<Produto>.Update.Combine(updateDefinitions);

            await _productCollection.UpdateOneAsync(x => x.Id == id, combinedUpdates);
        }

        public async Task RemoveAsync(string id)
        {
            await _productCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
