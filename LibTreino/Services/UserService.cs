using LibTreino.Data;
using LibTreino.Models;
using LibTreino.Models.ViewModels.Usuario;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibTreino.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(IOptions<ConfigDatabaseSettings> databaseOptions)
        {
            var mongoClient = new MongoClient(databaseOptions.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseOptions.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<User>(databaseOptions.Value.UserCollectionName);
        }

        public async Task<List<User>> GetAsync()
        {
            return await _userCollection.Find(x => true).ToListAsync();
        }

        public async Task<User> GetAsync(string id)
        {
            return await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> CreateAsync(CreateUser newUser)
        {

            var user = new User
            {
                Name = newUser.Name,
                Email = newUser.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password),
                Phone = newUser.Phone ?? string.Empty
            };

            await _userCollection.InsertOneAsync(user);

            return user;
        }

        public async Task UpdateAsync(string id, UpdateUser updateUser)
        {
            var updateDefinitions = new List<UpdateDefinition<User>>();

            if (!string.IsNullOrEmpty(updateUser.Name))
                updateDefinitions.Add(Builders<User>.Update.Set(p => p.Name, updateUser.Name));

            if (!string.IsNullOrEmpty(updateUser.Phone))
                updateDefinitions.Add(Builders<User>.Update.Set(p => p.Phone, updateUser.Phone));

            if (!string.IsNullOrEmpty(updateUser.Password))
                updateDefinitions.Add(Builders<User>.Update.Set(p => p.Password, BCrypt.Net.BCrypt.HashPassword(updateUser.Password)));

            if (updateUser.Listas != null)
                updateDefinitions.Add(Builders<User>.Update.Set(p => p.Listas, updateUser.Listas));

            var combinedUpdates = Builders<User>.Update.Combine(updateDefinitions);

            await _userCollection.UpdateOneAsync(x => x.Id == id, combinedUpdates);
        }

        public async Task RemoveAsync(string id)
        {
            await _userCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
