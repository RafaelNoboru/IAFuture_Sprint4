using System.ComponentModel.DataAnnotations;

namespace Sprint4.Dtos
{
    public class ClienteLoginDto
    {
        [Required(ErrorMessage = "O username é obrigatório.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; }
    }
}
