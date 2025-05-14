using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GustaffTarefas.Models
{
    public class TarefaModel
    {

        [Key]
        public int TarefaId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O nome da tarefa não pode exceder 100 caracteres.")]
        [Display(Name = "Nome da Tarefa")]
        public string TarefaNome { get; set; }

        public bool Feito { get; set; } = false;

        [Display(Name = "Data de vencimento da tarefa")]
        [Required(ErrorMessage = "E nescessario informar uma data de cencimento")]
        public DateTime Vencimento { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        [Display(Name = "Descreva a tarefa")]
        public string? Descricao { get; set; }

        public enum PrioridadeEnum
        {
            Baixa, //0
            Media, //1
            Alta //2
        }

        public PrioridadeEnum Prioridade { get; set; }

        //idetificar o usuario

        [Required]
        public string UserId { get; set; } = "df";

        [ForeignKey("UserId")]
        [ValidateNever]
        public virtual IdentityUser User { get; set; }
    }

}