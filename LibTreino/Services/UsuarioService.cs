using LibTreino.Data;
using LibTreino.Models;
using LibTreino.Models.ViewModels.Usuario;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibTreino.Services
{
    public class UsuarioService
    {
        private readonly IMongoCollection<Usuario> _userCollection;

        public UsuarioService(IOptions<ConfigDatabaseSettings> databaseOptions)
        {
            var mongoClient = new MongoClient(databaseOptions.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseOptions.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<Usuario>(databaseOptions.Value.UserCollectionName);
        }

        public async Task<List<Usuario>> GetAsync()
        {
            return await _userCollection.Find(x => true).ToListAsync();
        }

        public async Task<Usuario> GetAsync(string id)
        {
            return await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Usuario> CreateAsync(CreateUser newUser)
        {

            var user = new Usuario
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
            var updateDefinitions = new List<UpdateDefinition<Usuario>>();

            if (!string.IsNullOrEmpty(updateUser.Name))
                updateDefinitions.Add(Builders<Usuario>.Update.Set(p => p.Name, updateUser.Name));

            if (!string.IsNullOrEmpty(updateUser.Phone))
                updateDefinitions.Add(Builders<Usuario>.Update.Set(p => p.Phone, updateUser.Phone));

            if (!string.IsNullOrEmpty(updateUser.Password))
                updateDefinitions.Add(Builders<Usuario>.Update.Set(p => p.Password, BCrypt.Net.BCrypt.HashPassword(updateUser.Password)));

            if (updateUser.Listas != null)
                updateDefinitions.Add(Builders<Usuario>.Update.Set(p => p.Listas, updateUser.Listas));

            var combinedUpdates = Builders<Usuario>.Update.Combine(updateDefinitions);

            await _userCollection.UpdateOneAsync(x => x.Id == id, combinedUpdates);
        }

        public async Task RemoveAsync(string id)
        {
            await _userCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _userCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
