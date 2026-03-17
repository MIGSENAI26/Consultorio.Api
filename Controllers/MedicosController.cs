using Consultorio.Api.Data;
using Consultorio.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicosController : ControllerBase
{
    private readonly AppDbContext _context;

    public MedicosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetMedicos()
    {
        var medicos = await _context.Medicos.Include(m => m.Consultorio).ToListAsync();
        return Ok(medicos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMedico(int id)
    {
        var medico = await _context.Medicos
            .Include(m => m.Consultorio)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (medico == null) return NotFound();
        return Ok(medico);
    }

    [HttpPost]
    public async Task<ActionResult<Medico>> PostMedico(Medico medico)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Medicos.Add(medico);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMedico), new { id = medico.Id }, medico);
    }

    [HttpPut]
    public async Task<IActionResult> PutMedico(Medico medico)
    {
        var medicoExistente = await _context.Medicos.FindAsync(medico.Id);
        if (medicoExistente == null)
        {
            return NotFound();
        }
        medicoExistente.Nome = medico.Nome;
        medicoExistente.Crm = medico.Crm;
        medicoExistente.ConsultoriooId = medico.ConsultoriooId;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedico(int id)
    {
        var medico = await _context.Medicos.FindAsync(id);
        if (medico == null)
        {
            return NotFound();
        }
        _context.Medicos.Remove(medico);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}