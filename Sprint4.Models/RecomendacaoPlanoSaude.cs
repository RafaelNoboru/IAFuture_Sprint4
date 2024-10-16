using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.IdGenerators;
using System.ComponentModel.DataAnnotations;

namespace Sprint4.Models
{
    [Table("iafuture_csharp_recomendacoes")]
    //[Collection("recomendacoes")]
    public class RecomendacaoPlanoSaude
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public int Id { get; set; }

        //[BsonElement("cliente")]
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        //[BsonElement("cliente")]
        [ForeignKey("PlanoSaude")]
        public int PlanoSaudeId { get; set; }
        public PlanoSaude PlanoSaude { get; set; }

        public DateTime DataRecomendacao { get; set; }
    }
}
