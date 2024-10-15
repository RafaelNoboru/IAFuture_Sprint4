using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint4.Models
{
    public class RecomendacaoPlanoSaude
    {   
        public int Id { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [ForeignKey("PlanoSaude")]
        public int PlanoSaudeId { get; set; }
        public PlanoSaude PlanoSaude { get; set; }

        public DateTime DataRecomendacao { get; set; }
    }
}
