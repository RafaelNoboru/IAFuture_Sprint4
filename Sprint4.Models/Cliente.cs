using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.EntityFrameworkCore;

namespace Sprint4.Models
{
    [Table("iafuture_csharp_clientes")]
    //[Collection("clientes")]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public int Id { get; set; }

        //[BsonElement("nome")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        //[BsonElement("idade")]
        [Required(ErrorMessage = "A idade é obrigatória.")]
        [Range(0, 120, ErrorMessage = "A idade deve estar entre 0 e 120 anos.")]
        public int Idade { get; set; }

        //[BsonElement("sexo")]
        [Required(ErrorMessage = "O sexo é obrigatório.")]
        [RegularExpression("^(Masculino|Feminino|Outro)$", ErrorMessage = "Sexo deve ser 'Masculino', 'Feminino' ou 'Outro'.")]
        public string Sexo { get; set; }

        //[BsonElement("profissao")]
        [Required(ErrorMessage = "A profissão é obrigatória.")]
        [StringLength(50, ErrorMessage = "A profissão não pode exceder 50 caracteres.")]
        public string Profissao { get; set; }

        //[BsonElement("estadoSaude")]
        [Required(ErrorMessage = "O estado de saúde é obrigatório.")]
        [StringLength(50, ErrorMessage = "O estado de saúde não pode exceder 50 caracteres.")]
        public string EstadoSaude { get; set; }
    }
}
