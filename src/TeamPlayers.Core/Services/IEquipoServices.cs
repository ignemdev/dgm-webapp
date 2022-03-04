using System.Collections.Generic;
using System.Threading.Tasks;
using TeamPlayers.Core.Entities;

namespace TeamPlayers.Core.Services
{
    public interface IEquipoServices
    {
        Task<IEnumerable<Equipo>> GetAllEquipos();
        Task<Equipo> AddEquipo(Equipo Equipo);
        Task<Equipo> UpdateEquipo(Equipo Equipo);
        Task<Equipo> GetEquipoById(int EquipoId);
        Task<Equipo> DeleteEquipoById(int equipoId);
        Task<Equipo> ToggleEquipoById(int equipoId);
    }
}
