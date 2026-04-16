using Medicos.Backend.Data.Models;
using Medicos.Backend.DTOs;

namespace Medicos.Backend.Repositories.Interfaces;

public interface IMedicoRepository
{
    Task<List<MedicoOutputDto>> GetAllMedicosAsync();
    Task<MedicoOutputDto> GetMedicoByIdAsync(int id);
    Task<Medico> CreateMedicoAsync(MedicoCreateDto dto);
    Task<bool> UpdateMedicoAsync(int id, MedicoCreateDto dto);
    Task<bool> DeleteMedicoAsync(int id);
}