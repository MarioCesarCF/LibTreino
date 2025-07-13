using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibTreino.Models
{
    public class Produto
    {
        // Notações para o banco criar a string do id. Estudar formas melhores de fazer isso
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
    }
}
