using System.Collections.Generic;
using System.Threading.Tasks;
using TeamPlayers.Core.Entities;

namespace TeamPlayers.Core.Services
{
    public interface IJugadorServices
    {
        Task<IEnumerable<Jugador>> GetAllJugadores();
        Task<Jugador> AddJugador(Jugador jugador);
        Task<Jugador> UpdateJugador(Jugador jugador);
        Task<Jugador> GetJugadorById(int jugadorId);
        Task DeleteJugadorById(int jugadorId);
    }
}
