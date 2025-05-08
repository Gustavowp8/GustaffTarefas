using Microsoft.AspNetCore.Identity;
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
        public string TarefaNome { get; set; }

        public bool Feito { get; set; }

        public DateTime Vencimento { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public string Descricao { get; set; }

        public enum PrioridadeEnum
        {
            Baixa,
            Media,
            Alta
        }

        public PrioridadeEnum Prioridade { get; set; }

        //idetificar o usuario

        [Required]
        public string UserId { get; set; } = "df";

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
    }

}
