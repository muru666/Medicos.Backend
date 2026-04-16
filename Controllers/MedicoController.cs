using Medicos.Backend.DTOs;
using Medicos.Backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Medicos.Backend.Data.Models;

namespace Medicos.Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MedicoController : ControllerBase
{
    private readonly IMedicoRepository _context;
    private readonly IEspecialidadRepository _especialidadContext;

    public MedicoController(IMedicoRepository context, IEspecialidadRepository especialidadContext)
    {
        _context = context;
        _especialidadContext = especialidadContext;
    }

    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        try
        {
            var dtos = await _context.GetAllMedicosAsync();
            return Ok(dtos);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {

        try
        {
            var dto = await _context.GetMedicoByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MedicoCreateDto dto)
    {



        try
        {
            var especialidad = await _especialidadContext.GetEspecialidadByIdAsync(dto.EspecialidadId);
            if (especialidad is null)
                return BadRequest("La especialidad no existe.");

            var medico = await _context.CreateMedicoAsync(dto);
            var outputDto = new MedicoOutputDto(
                dto.Nombre,
                dto.Apellido,
                especialidad.Nombre);

            return CreatedAtAction(
                nameof(GetById),
                new { id = medico.Id },
                outputDto);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }


        /*
         *var especialidad = await _context.CreateEspecialidadAsync(dto);
           var resultDto = new EspecialidadDto(especialidad.Nombre);

           return CreatedAtAction(
               nameof(GetById),
               new { id = especialidad.Id },
               resultDto);
         *
         */
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] MedicoCreateDto dto)
    {

        try
        {
            var updated = await _context.UpdateMedicoAsync(id, dto);
            return updated ? NotFound() : NoContent();

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {

        try
        {
            var deleted = await _context.DeleteMedicoAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}