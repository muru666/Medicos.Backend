using Medicos.Backend.Data.Models;
using Medicos.Backend.DTOs;

namespace Medicos.Backend.Repositories.Interfaces;

public interface IEspecialidadRepository
{
    Task<List<EspecialidadDto>> GetAllEspecialidadesAsync();
    Task<EspecialidadDto> GetEspecialidadByIdAsync(int id);
    Task<Especialidad> CreateEspecialidadAsync(EspecialidadDto dto);
    Task<bool> UpdateEspecialidadAsync(int id, EspecialidadDto dto);
    Task<bool> DeleteEspecialidadAsync(int id);
}