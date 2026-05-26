// Data/TarefaContext.cs
// Aqui é o Contexto do Banco de Dados 
using Microsoft.EntityFrameworkCore;
using TarefaApiIVS.Models; // Não esqueça de ajustar o nomespace caso necessário.

namespace TarefaApiIVS.Data
{
    public class TarefaContext : DbContext // A classe TarefaContext herda tudo da classe DbContext.

    { // Construtor que recebe as configurações do banco de dados
        public  TarefaContext(DbContextOptions<TarefaContext> options) : base(options)
           {

           }
    
    public DbSet<Tarefa> Tarefas { get; set; } = null!;
        //DbSet<Tarefa> respresenta a tabela a ser criada.
    }


}
