﻿using System.ComponentModel.DataAnnotations;

namespace Sprint4.Dtos
{
    public class ClienteUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O username é obrigatório.")]
        [StringLength(50, ErrorMessage = "O username não pode exceder 50 caracteres.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "A idade é obrigatória.")]
        [Range(0, 120, ErrorMessage = "A idade deve estar entre 0 e 120 anos.")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "O sexo é obrigatório.")]
        [RegularExpression("^(Masculino|Feminino|Outro)$", ErrorMessage = "Sexo deve ser 'Masculino', 'Feminino' ou 'Outro'.")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "A profissão é obrigatória.")]
        [StringLength(50, ErrorMessage = "A profissão não pode exceder 50 caracteres.")]
        public string Profissao { get; set; }

        [Required(ErrorMessage = "O estado de saúde é obrigatório.")]
        [StringLength(50, ErrorMessage = "O estado de saúde não pode exceder 50 caracteres.")]
        public string EstadoSaude { get; set; }

        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Senha { get; set; }
    }
}