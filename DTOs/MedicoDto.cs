namespace Medicos.Backend.DTOs;

public record MedicoCreateDto(string Nombre, string Apellido, int EspecialidadId);

public record MedicoOutputDto(string Nombre, string Apellido, string Especialidad);