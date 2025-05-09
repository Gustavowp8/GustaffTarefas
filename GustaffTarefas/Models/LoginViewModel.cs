using System.ComponentModel.DataAnnotations;

namespace GustaffTarefas.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Entre com o e-mail ou nome de usuario")]
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }
    }
}