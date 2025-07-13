using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LibTreino.Models
{
    public class ListaCompras
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<ItemLista> ItemLista { get; set; } = new List<ItemLista>();
    }
}
