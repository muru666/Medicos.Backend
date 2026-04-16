using Medicos.Backend.Data.Models;
using Medicos.Backend.DTOs;
using Medicos.Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medicos.Backend.Repositories;

public class EspecialidadRepository : IEspecialidadRepository
{
    private readonly TestingContext _context;

    public EspecialidadRepository(TestingContext context)
    {
        _context = context;
    }
    
    public async Task<List<EspecialidadDto>> GetAllEspecialidadesAsync()
    {
        return await _context.Especialidads
            .Select(e => new EspecialidadDto(e.Nombre))
            .ToListAsync();
        
    }

    public async Task<EspecialidadDto?> GetEspecialidadByIdAsync(int id)
    {
        return await _context.Especialidads
            .Where(e => e.Id == id)
            .Select(e => new EspecialidadDto(e.Nombre))
            .FirstOrDefaultAsync();

    }

    public async Task<Especialidad> CreateEspecialidadAsync(EspecialidadDto dto)
    {
        var especialidadNueva = new Especialidad()
        {
            Nombre = dto.Nombre
        };
        await _context.Especialidads.AddAsync(especialidadNueva);
        await _context.SaveChangesAsync();
        return especialidadNueva;
    }

    public async Task<bool> UpdateEspecialidadAsync(int id, EspecialidadDto dto)
    {
        var rows = await _context.Especialidads
            .Where(e => e.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(e => e.Nombre, dto.Nombre));
        return rows > 0;
    }

    public async Task<bool> DeleteEspecialidadAsync(int id)
    {
        var rows = await _context.Especialidads
            .Where(e => e.Id == id)
            .ExecuteDeleteAsync();
        return rows > 0;
    }
}