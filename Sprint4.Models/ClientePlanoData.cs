using Sprint4.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint4.Models
{
    public class ClientePlanoData
    {
        public float Idade { get; set; }
        public Sexo Sexo { get; set; }
        public string Profissao { get; set; }
        public EstadoSaude EstadoSaude { get; set; }
        public float RendaMensal { get; set; }
        public string PlanoRecomendado { get; set; }
    }
}

