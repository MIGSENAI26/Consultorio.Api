using Consultorio.Api.Data;
using Consultorio.Api.Models;
using Consultorio.Api.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultorioosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ViaCepService _viaCepService;

        public ConsultorioosController(AppDbContext context, ViaCepService viaCepService)
        {
            _context = context;
            _viaCepService = viaCepService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsultorio()
        {
            var consultorioos = await _context.Consultorioos.ToListAsync();
            return Ok(consultorioos);
        }

        [HttpPost]
        public async Task<ActionResult<Consultorioo>> PostConsultorio(Consultorioo consultorioo)
        {
            var endereco = await _viaCepService.BuscarEnderecoAsync(consultorioo.Cep);
            if (endereco != null)
            {
                consultorioo.Logradouro = endereco.logradouro;
                consultorioo.Bairro = endereco.bairro;
                consultorioo.Localidade = endereco.localidade;
                consultorioo.Uf = endereco.uf;
            }
            else
            {
                BadRequest("CEP não encontrado");
            }

                _context.Consultorioos.Add(consultorioo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetConsultorio), new { id = consultorioo.Id }, consultorioo);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Consultorioo>> GetConsultorio(int id)
        {
            var consultorioo = await _context.Consultorioos.FindAsync(id);
            if (consultorioo == null)
            {
                return NotFound();
            }
            return consultorioo;
        }

        [HttpPut]
        public async Task<IActionResult> PutConsultorio(Consultorioo consultorioo)
        {
            var consultorioExistente = await _context.Consultorioos.FindAsync(consultorioo.Id);
            var endereco = await _viaCepService.BuscarEnderecoAsync(consultorioo.Cep);
            if (endereco != null)
                if (consultorioExistente == null)
            {
                return NotFound();
            }
            consultorioExistente.Nome = consultorioo.Nome;
            consultorioExistente.Cep = consultorioo.Cep;
            consultorioExistente.Logradouro = endereco.logradouro;
            consultorioExistente.Bairro = endereco.bairro;
            consultorioExistente.Localidade = endereco.localidade;
            consultorioExistente.Uf = endereco.uf;
            consultorioExistente.Numero = consultorioo.Numero;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsultorio(int id)
        {
            var consultorioo = await _context.Consultorioos.FindAsync(id);
            if (consultorioo == null)
            {
                return NotFound();
            }
            _context.Consultorioos.Remove(consultorioo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
