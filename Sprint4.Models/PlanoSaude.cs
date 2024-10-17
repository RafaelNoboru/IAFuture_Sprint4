using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;
using Sprint4.Models.Enum;
using MongoDB.Bson.Serialization.IdGenerators;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint4.Models
{
    [Table("iafuture_csharp_planos")]
    //[Collection("planos")]
    public class PlanoSaude
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public int Id { get; set; }

        //[BsonElement("nome")]
        [Required(ErrorMessage = "O nome do plano de saúde é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do plano deve ter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }

        //[BsonElement("preco")]
        [Required(ErrorMessage = "O preço é obrigatório.")]
        public double Preco { get; set; }

        //[BsonElement("tipoPlano")]
        [Required(ErrorMessage = "O tipo de plano é obrigatório.")]
        [EnumDataType(typeof(TipoPlano), ErrorMessage = "Tipo de plano inválido.")]
        public TipoPlano TipoPlano { get; set; }

        //[BsonElement("cobertura")]
        [Required(ErrorMessage = "A cobertura é obrigatória.")]
        [StringLength(500, ErrorMessage = "A cobertura não pode exceder 500 caracteres.")]
        public string Cobertura { get; set; }

        [Required(ErrorMessage = "O público alvo é obrigatório.")]
        [EnumDataType(typeof(PublicoAlvo), ErrorMessage = "Tipo de público inválido.")]
        public PublicoAlvo PublicoAlvo { get; set; }  
    }
}
