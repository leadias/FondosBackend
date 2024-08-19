using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FondosApplication.Models
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;

        public string Nombre { get; set; } = null!;
        public string identificacion { get; set; } = null!;

        public double saldo { get; set; }

    }
}
