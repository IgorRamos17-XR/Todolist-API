//Model/Tarefas.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// O nomespace pode varias de acordo com o nome do seu projeto se atente nisso.
namespace TarefaApiIVS.Models
{
    public class Tarefa
    {
        [Key] // Chave primaria 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//DatabaseGenerated = Indica que o valor será gerado automaticamente pelo banco de dados.
        // DatabaseGeneratedOption.Identity = Faz com que o bando de dados gere automaticamenteo valoe,
        // Não precisamos informar o valor do ID.

        public int Id { get; set; } // Se não souber isso aqui, ai você me complica vai estudar lógica de novo.

        // Required = Torna-se o preenchimento do campo obrigatorio,o usuário deve preencher este campo.
        [Required(ErrorMessage = "O título da tarefa é obrigatório.")]
        [StringLength(255, ErrorMessage = "O título não pode exeder 255 caracteres.")]
        // StringLength(255)] = O texto pode ter no máximo 255 caracteres.

        public string? Titulo { get; set; }
        // O ( ? ) indica que a variável pode receber valor nulo (null).
        

        [StringLength(1000,ErrorMessage ="A descrição não pode exeder 1000 caracteres.")]
        public string? Descrição { get; set; }
       
        public bool Concluida { get; set; } = false;
        
        [Display(Name ="Data de Criação")]
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        

        [Display(Name ="Data de Vencimento")]
        public DateTime? DataVencimento { get; set; }


        [Required(ErrorMessage ="Selecione uma categoria.")]
        [StringLength(50)]
        public string? Categoria { get; set; }

        [Required(ErrorMessage = "Selecione uma prioridade.")]
        public Prioridade Prioridade { get; set; } = Prioridade.Media;

        // Apenas para exibição
        [NotMapped]
        public string StatusTexto => Concluida ? "Sim" : "Não";

   
    }
}
