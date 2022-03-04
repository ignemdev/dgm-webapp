using System;
using System.Threading.Tasks;
using TeamPlayers.Core.Entities;

namespace TeamPlayers.Core.Repositories
{
    public interface IJugadorRepository : IRepository<Jugador>
    {
        Task<Jugador> UpdateAsync(Jugador estado);
        Task<Jugador> ChangeStatus(Jugador jugador, TipoEstado estado);
        Task<Jugador> AssignTeam(Jugador jugador);
    }
}
