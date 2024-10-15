using Sprint4.Models.Enum;

namespace Sprint4.Models
{
    public class PlanoSaude
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public TipoPlano TipoPlano { get; set; } 
        public string Cobertura { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataAtualizacao { get; set; }
    }
}
