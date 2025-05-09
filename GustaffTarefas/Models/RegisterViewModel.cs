using System.ComponentModel.DataAnnotations;

namespace GustaffTarefas.Models
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Nome de usuário é obrigatório")]
        [Display(Name = "Nome de Usuário")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Display(Name = "Endereço de Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        [Display(Name = "Senha")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string? ConfirmPassword
        {
            get; set;

        }
    }
}