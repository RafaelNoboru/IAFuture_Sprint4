using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Sprint4.Models
{
    [Collection("recomendacoes")]
    public class RecomendacaoPlanoSaude
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId Id { get; set; }

        [BsonElement("cliente")]
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [BsonElement("cliente")]
        [ForeignKey("PlanoSaude")]
        public int PlanoSaudeId { get; set; }
        public PlanoSaude PlanoSaude { get; set; }

        public DateTime DataRecomendacao { get; set; }
    }
}
