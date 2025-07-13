using LibTreino.Data;
using LibTreino.Enums;
using LibTreino.Models;
using LibTreino.Models.ViewModels.Produto;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibTreino.Services
{
    public class ProductService
    {
        //Configuração de conexão do service com o banco.
        //Procurar como fazer isso de forma melhor
        private readonly IMongoCollection<Product> _productCollection;

        public ProductService(IOptions<ConfigDatabaseSettings> productDatabaseOptions) 
        {
            var mongoClient = new MongoClient(productDatabaseOptions.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(productDatabaseOptions.Value.DatabaseName);

            _productCollection = mongoDatabase.GetCollection<Product>(productDatabaseOptions.Value.ProductCollectionName);
        }

        public async Task<List<Product>> GetAsync()
        {
            return await _productCollection.Find(x => true).ToListAsync();
        }

        public async Task<Product> GetAsync(string id)
        {
            return await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> CreateAsync(ProdutoCreateRequest novoProduto)
        {
            var produto = new Product
            {
                Name = novoProduto.Name,
                Amount = novoProduto.Amount,
                Unity = novoProduto.Unity
            };

            produto.Situation = Situation.Ativo;

            await _productCollection.InsertOneAsync(produto);

            return produto;
        }

        public async Task UpdateAsync(string id, UpdateProduct produtoAlterado)
        {
            var updateDefinitions = new List<UpdateDefinition<Product>>();

            if (!string.IsNullOrEmpty(produtoAlterado.Name))
                updateDefinitions.Add(Builders<Product>.Update.Set(p => p.Name, produtoAlterado.Name));

            if (produtoAlterado.Amount != null)
                updateDefinitions.Add(Builders<Product>.Update.Set(p => p.Amount, produtoAlterado.Amount));

            if (produtoAlterado.Unity != null)
                updateDefinitions.Add(Builders<Product>.Update.Set(p => p.Unity, produtoAlterado.Unity));

            if (produtoAlterado.Situation != null)
                updateDefinitions.Add(Builders<Product>.Update.Set(p => p.Situation, produtoAlterado.Situation));

            var combinedUpdates = Builders<Product>.Update.Combine(updateDefinitions);

            await _productCollection.UpdateOneAsync(x => x.Id == id, combinedUpdates);
        }

        public async Task RemoveAsync(string id)
        {
            await _productCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
