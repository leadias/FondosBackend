using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FondosApplication.Models
{
    public class Fondo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;

        public string? ClienteId { get; set; } = null;
        public string Nombre { get; set; } = null!;

        public double Monto { get; set; }

        public string Categoria { get; set; } = null!;
        public string? Estado { get; set; } 


    }
}
