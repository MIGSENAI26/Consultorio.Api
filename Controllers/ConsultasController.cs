using Consultorio.Api.Data;
using Consultorio.Api.DTOs;
using Consultorio.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConsultasController : ControllerBase
{
    private readonly AppDbContext _context;

    public ConsultasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetConsulta()
    {
        var consultas = await _context.Consultas.Include(c => c.Paciente).Include(c => c.Medico).ToListAsync();
        return Ok(consultas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MedicoResponseDto>> GetConsulta(int id)
    {
        var consulta = await _context.Consultas
            .Include(c => c.Paciente)
            .Include(c => c.Medico)
            .FirstOrDefaultAsync(m => m.Id == id);

        var c = await _context.Consultas.FindAsync(id);
        var m = await _context.Medicos.FindAsync(id);
        var p = await _context.Pacientes.FindAsync(id);

        var Test = consulta.Paciente.Nome;
        var Test2 = p.Nome;

        if (consulta == null) return NotFound("Consulta não encontrada!");
        var consultaDto = new ConsultaResponseDto
        {
            Id = consulta.Id,
            PacienteId = consulta.PacienteId,
            PacienteNome = consulta.Paciente?.Nome,
            MedicoId = consulta.MedicoId,
            MedicoNome = consulta.Medico?.Nome,
            DataHora = consulta.DataHora,
            Observacoes = consulta.Observacoes
        };
        return Ok(consultaDto);
    }

    [HttpPost]
    public async Task<ActionResult<Consulta>> PostConsulta(Consulta consulta)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Consultas.Add(consulta);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetConsulta), new { id = consulta.Id }, consulta);
    }

    [HttpPut]
    public async Task<IActionResult> PutConsulta(Consulta consulta)
    {
        var consultaExistente = await _context.Consultas.FindAsync(consulta.Id);
        if (consultaExistente == null)
        {
            return NotFound();
        }
        consultaExistente.PacienteId = consulta.PacienteId;
        consultaExistente.MedicoId = consulta.MedicoId;
        consultaExistente.DataHora = consulta.DataHora;
        consultaExistente.Observacoes = consulta.Observacoes;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConsulta(int id)
    {
        var consulta = await _context.Consultas.FindAsync(id);
        if (consulta == null)
        {
            return NotFound();
        }
        _context.Consultas.Remove(consulta);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}