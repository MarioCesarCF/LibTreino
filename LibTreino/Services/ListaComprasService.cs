using LibTreino.Data;
using LibTreino.Enums;
using LibTreino.Models;
using LibTreino.Models.ViewModels.Lista;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibTreino.Services
{
    public class ListaComprasService
    {
        private readonly IMongoCollection<ListaCompras> _shoppingListCollection;

        public ListaComprasService(IOptions<ConfigDatabaseSettings> shoppingListDatabaseOptions)
        {
            var mongoClient = new MongoClient(shoppingListDatabaseOptions.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(shoppingListDatabaseOptions.Value.DatabaseName);

            _shoppingListCollection = mongoDatabase.GetCollection<ListaCompras>(shoppingListDatabaseOptions.Value.ShoppingListCollectionName);
        }

        public async Task<List<ListaCompras>> GetAsync()
        {
            return await _shoppingListCollection.Find(x => true).ToListAsync();
        }

        public async Task<ListaCompras> GetAsync(string id)
        {
            return await _shoppingListCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ListaCompras> CreateAsync(CriaListaCompras newshoppingList)
        {
            var shoppingList = new ListaCompras
            {
                Nome = newshoppingList.Nome
            };

            await _shoppingListCollection.InsertOneAsync(shoppingList);

            return shoppingList;
        }

        public async Task UpdateAsync(AtualizaListaCompras updateShoppingList)
        {
            var updateDefinitions = new List<UpdateDefinition<ListaCompras>>();

            if (!string.IsNullOrEmpty(updateShoppingList.Nome))
                updateDefinitions.Add(Builders<ListaCompras>.Update.Set(p => p.Nome, updateShoppingList.Nome));
            if (updateShoppingList.ItemLista.Count() > 0)
                updateDefinitions.Add(Builders<ListaCompras>.Update.Set(p => p.ItemLista, updateShoppingList.ItemLista));

            var combinedUpdates = Builders<ListaCompras>.Update.Combine(updateDefinitions);

            await _shoppingListCollection.UpdateOneAsync(x => x.Id == updateShoppingList.Id, combinedUpdates);
        }

        public async Task RemoveAsync(string id)
        {
            await _shoppingListCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task AddProductAsync(AdicionaProdutoNaLista dto)
        {
            dto.ItemLista.Id = Guid.NewGuid().ToString();
            dto.ItemLista.Situacao = 1;

            var filter = Builders<ListaCompras>.Filter.Eq(s => s.Id, dto.ListaComprasId);
            var update = Builders<ListaCompras>.Update.Push(s => s.ItemLista, dto.ItemLista);

            await _shoppingListCollection.UpdateOneAsync(filter, update);
        }

        public async Task RemoveProductAsync(RemoveProdutoLista dto)
        {
            var filter = Builders<ListaCompras>.Filter.Eq(s => s.Id, dto.ListaComprasId);
            var update = Builders<ListaCompras>.Update.PullFilter(s => s.ItemLista, item => item.Id == dto.Id);
            await _shoppingListCollection.UpdateOneAsync(filter, update);
        }

        //public async Task UpdateItemAsync(AtualizaItemListaDto dto)
        //{
        //    var lista = await GetAsync(dto.ListaComprasId);

        //    if (lista == null)
        //        throw new Exception("Lista de compras não encontrada.");

        //    var item = lista.ItemLista.FirstOrDefault(i => i.Id == dto.Id);

        //    if (item == null)
        //        throw new Exception("Item da lista não encontrado.");

        //    item.Nome = dto.Nome;
        //    item.Quantidade = dto.Quantidade;
        //    item.Unidade = dto.Unidade;
        //    item.Situacao = dto.Situacao;

        //    await _listaComprasRepository.UpdateAsync(lista);
        //}
    }
}
