using LibTreino.Data;
using LibTreino.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using LibTreino.Models.ViewModels.Lista;

namespace LibTreino.Services
{
    public class ShoppingListService
    {
        private readonly IMongoCollection<ShoppingList> _shoppingListCollection;

        public ShoppingListService(IOptions<ConfigDatabaseSettings> shoppingListDatabaseOptions)
        {
            var mongoClient = new MongoClient(shoppingListDatabaseOptions.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(shoppingListDatabaseOptions.Value.DatabaseName);

            _shoppingListCollection = mongoDatabase.GetCollection<ShoppingList>(shoppingListDatabaseOptions.Value.ShoppingListCollectionName);
        }

        public async Task<List<ShoppingList>> GetAsync()
        {
            return await _shoppingListCollection.Find(x => true).ToListAsync();
        }

        public async Task<ShoppingList> GetAsync(string id)
        {
            return await _shoppingListCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ShoppingList> CreateAsync(CreateShoppingList newshoppingList)
        {
            var shoppingList = new ShoppingList
            {
                Title = newshoppingList.Title,
                Products = newshoppingList.Products
            };

            await _shoppingListCollection.InsertOneAsync(shoppingList);

            return shoppingList;
        }

        public async Task UpdateAsync(string id, UpdateShoppingList updateShoppingList)
        {
            var updateDefinitions = new List<UpdateDefinition<ShoppingList>>();

            if (!string.IsNullOrEmpty(updateShoppingList.Title))
                updateDefinitions.Add(Builders<ShoppingList>.Update.Set(p => p.Title, updateShoppingList.Title));

            if (updateShoppingList.Products != null)
                updateDefinitions.Add(Builders<ShoppingList>.Update.Set(p => p.Products, updateShoppingList.Products));

            var combinedUpdates = Builders<ShoppingList>.Update.Combine(updateDefinitions);

            await _shoppingListCollection.UpdateOneAsync(x => x.Id == id, combinedUpdates);
        }

        public async Task RemoveAsync(string id)
        {
            await _shoppingListCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
