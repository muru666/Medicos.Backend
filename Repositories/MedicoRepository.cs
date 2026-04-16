using Medicos.Backend.Data.Models;
using Medicos.Backend.DTOs;
using Medicos.Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medicos.Backend.Repositories;

public class MedicoRepository: IMedicoRepository
{
    private readonly TestingContext _context;

    public MedicoRepository(TestingContext context)
    {
        _context = context;
    }
    
    public async Task<List<MedicoOutputDto>> GetAllMedicosAsync()
    {
        return await _context.Medicos
            .Select(m => new MedicoOutputDto(m.Nombre, m.Apellido, m.EspecialidadNavigation.Nombre))
            .ToListAsync();
    }

    public async Task<MedicoOutputDto?> GetMedicoByIdAsync(int id)
    {
        return await _context.Medicos
            .Where(m => m.Id == id)
            .Select(m => new MedicoOutputDto(m.Nombre, m.Apellido, m.EspecialidadNavigation.Nombre))
            .FirstOrDefaultAsync();
    }

    public async Task<Medico> CreateMedicoAsync(MedicoCreateDto dto)
    {
        Medico medicoNuevo = new Medico()
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Especialidad = dto.EspecialidadId
        };
        await _context.Medicos.AddAsync(medicoNuevo);
        await _context.SaveChangesAsync();
        return medicoNuevo;
    }

    public async Task<bool> UpdateMedicoAsync(int id, MedicoCreateDto dto)
    {
        
        var rows = await _context.Medicos
            .Where(m => m.Id == id)
            .ExecuteUpdateAsync<Medico>(s => s
                .SetProperty(m => m.Nombre, dto.Nombre)
                .SetProperty(m => m.Apellido, dto.Apellido)
                .SetProperty(m => m.Especialidad, dto.EspecialidadId)
            );
        return rows > 0;
    }

    public async Task<bool> DeleteMedicoAsync(int id)
    {
        var rows = await _context.Medicos
            .Where(m => m.Id == id)
            .ExecuteDeleteAsync();
        return rows > 0;
    }
}