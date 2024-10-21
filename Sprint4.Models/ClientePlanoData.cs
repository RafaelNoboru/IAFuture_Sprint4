using Microsoft.ML.Data;
using Sprint4.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint4.Models
{
    public class ClientePlanoData
    {
        [LoadColumn(0)]
        public float Idade { get; set; }

        [LoadColumn(1)]
        public float NumeroConsultas { get; set; }

        [LoadColumn(2)]
        public float Internacoes { get; set; }

        [LoadColumn(3)]
        public string EstadoCivil { get; set; }

        [LoadColumn(4)]
        public string Cidade { get; set; }

        [LoadColumn(5)]
        public string Plano { get; set; } 
    }
}

