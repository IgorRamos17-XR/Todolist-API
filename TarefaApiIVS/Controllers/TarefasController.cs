using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TarefaApiIVS.Data; // Ajuste o namespace
using TarefaApiIVS.Models; // Ajuste o namespace


namespace TarefaApiIVS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]// Indica que essa classe é um controlador de API.
    public class TarefasController : ControllerBase // Fornece métodos úteis para APIs, basicamente os codigos de erros ou sucesso tipo (404 Not Found).
    {


        // Variável que guarda a conexão com o banco de dados.
        // readonly = Informa que o valor só pode ser atribuído no construtor.
        //Depois disso não pode ser alterado.
        private readonly TarefaContext _context;

        // Construtor
        public TarefasController(TarefaContext context)
        {
            _context = context;
    
        }


        // GET: api/tarefas, busca todos os dados da tabela 
        [HttpGet]
   public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefas()
        {
            return await _context.Tarefas.ToListAsync(); // Retorna la no suegger a lista JSON dos arquivos do banco de dados.
        }


        // GET: api/tarefas/5 , busca pelo (ID)
        [HttpGet("{id}")]

        public async Task<ActionResult<Tarefa>> GetTarefa(int id) // Recebe o Id 
        {

            // Observção os EntityFrameworkCore converte toda essa parte em comandos MySQL la no seu banco de dados automaticamente.


            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return NotFound(); // Se não existir retorna um erro.
            return tarefa;
        }

        // POST: api/tarefas, cria tarefas novas. 
        [HttpPost]
        public async Task<ActionResult<Tarefa>> PostTarefa(Tarefa tarefa)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefa);
        }

        // PUT: api/tarefas/5, atualiza tarefa sempre pelo (ID)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarefa(int id, Tarefa tarefa)
        {
            if (id != tarefa.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Entry(tarefa).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarefaExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        // DELETE: api/tarefas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return NotFound();
            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool TarefaExists(int id)
        {
            return _context.Tarefas.Any(e => e.Id == id);
        }
    }
}




    