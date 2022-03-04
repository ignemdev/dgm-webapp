using System;
using System.Threading.Tasks;
using TeamPlayers.Core.Entities;

namespace TeamPlayers.Core.Repositories
{
    public interface IEquipoRepository : IRepository<Equipo>
    {
        Task<Equipo> UpdateAsync(Equipo estado);
        Task<Equipo> ChangeStatus(Equipo equipo, TipoEstado estado);
    }
}
