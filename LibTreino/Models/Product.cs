using LibTreino.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibTreino.Models
{
    public class Product
    {
        // Notações para o banco criar a string do id. Estudar formas melhores de fazer isso
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int Amount { get; set; }
        public Unity Unity { get; set; }
        public Situation Situation { get; set; }
    }
}
