using Consultorio.Api.Data;
using Consultorio.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PacientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaciente()
        {
            var pacientes = await _context.Pacientes.ToListAsync();
            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaciente(int id)
        {
            var paciente = _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return Ok(paciente);
        }

        [HttpPost]
        public async Task<ActionResult<Paciente>> PostPaciente(Paciente paciente) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPaciente), new { id = paciente.Id }, paciente);
        }

        [HttpPut]
        public async Task<IActionResult> PutPaciente(Paciente paciente)
        {
            var pacienteExistente = _context.Pacientes.Find(paciente.Id);
            if (pacienteExistente == null)
            {
                return NotFound();
            }
            pacienteExistente.Nome = paciente.Nome;
            pacienteExistente.Email = paciente.Email;
            pacienteExistente.Cpf = paciente.Cpf;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var paciente = _context.Pacientes.Find(id);
            if (paciente == null)
            {
                return NotFound();
            }
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}