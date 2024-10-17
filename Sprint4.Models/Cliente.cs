    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson.Serialization.IdGenerators;
    using MongoDB.EntityFrameworkCore;
using Sprint4.Models.Enum;

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

            [Required(ErrorMessage = "O username é obrigatório.")]
            [StringLength(50, ErrorMessage = "O username não pode exceder 50 caracteres.")]
            public string Usuario { get; set; }

            //[BsonElement("idade")]
            [Required(ErrorMessage = "A idade é obrigatória.")]
            [Range(0, 120, ErrorMessage = "A idade deve estar entre 0 e 120 anos.")]
            public int Idade { get; set; }

            //[BsonElement("sexo")]
            [Required(ErrorMessage = "O sexo é obrigatório.")]
            [EnumDataType(typeof(Sexo), ErrorMessage = "Tipo de sexo inválido.")]
            public Sexo Sexo { get; set; }

            //[BsonElement("profissao")]
            [Required(ErrorMessage = "A profissão é obrigatória.")]
            [StringLength(50, ErrorMessage = "A profissão não pode exceder 50 caracteres.")]
            public string Profissao { get; set; }

            //[BsonElement("estadoSaude")]
            [Required(ErrorMessage = "O estado de saúde é obrigatório.")]
            [EnumDataType(typeof(EstadoSaude), ErrorMessage = "Tipo de estado de saúde inválido.")]
            public EstadoSaude EstadoSaude { get; set; }

            [Required(ErrorMessage = "A senha é obrigatória.")]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
            public string SenhaHash { get; set; }
        }
    }
