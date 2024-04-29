using ListaTarefa.Context;
using ListaTarefa.Entities;
using ListaTarefa.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ListaTarefa.Controllers
{
    [ApiController]
    [Route("Tarefa")]
    public class TarefaController: ControllerBase
    {
        private readonly TarefaContext _context;

        public TarefaController(TarefaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CriarTarefa(Tarefa novaTarefa)
        {
            _context.Add<Tarefa>(novaTarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = novaTarefa.Id }, novaTarefa);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);
            if(tarefaBanco == null)
            {
                return NotFound("Id Incorreto");
            }
            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult ApagarTarefa(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);
            if (tarefaBanco == null)
            {
                return NotFound("Id Incorreto");
            }
            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();
            return Ok("Tarefa Deletada");
        }
        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefaBanco = _context.Tarefas.Where(t => t.Titulo.Contains(titulo));
            if (tarefaBanco.Count() == 0)
            {
                return NotFound("Tarefa Não Encontrada");
            }
            return Ok(tarefaBanco);
        }
        [HttpPut("{id}")]
        public IActionResult AtualizarTarefa(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);
            if (tarefaBanco == null)
            {
                return NotFound("Tarefa Não Encontrada");
            }

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;
            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();
            return Ok(tarefaBanco);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodasAstarefas()
        {
            var tarefasBanco = _context.Tarefas;
            if(tarefasBanco.Count() == 0)
            {
                return NotFound("Nenhuma Tarefa Cadastrada!");
            }
            return Ok(tarefasBanco);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterTarefaPorData(string data)
        {
            var tarefasBanco = _context.Tarefas.ToList().Where(t =>
             t.Data.Date.ToString("G").Contains(data)
         );

            if (tarefasBanco.Count() == 0)
            {
                return NotFound("Nenhuma tarefa encontrada para a data especificada.");
            }
            return Ok(tarefasBanco);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterTarefaPorStatus(EnumStatusTarefa status)
        {
            var tarefasBanco = _context.Tarefas.Where(t => t.Status == status);

            if (tarefasBanco.Count() == 0)
            {
                return NotFound("Nenhuma tarefa encontrada para o status especificada.");
            }
            return Ok(tarefasBanco);
        }
    }
}
