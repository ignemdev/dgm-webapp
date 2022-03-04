using System.Collections.Generic;
using System.Threading.Tasks;
using TeamPlayers.Core.Entities;

namespace TeamPlayers.Core.Services
{
    public interface IJugadorServices
    {
        Task<IEnumerable<Jugador>> GetAllJugadores();
        Task<IEnumerable<Jugador>> GetAllJugadoresByProps(Jugador? props = null);
        Task<Jugador> AddJugador(Jugador jugador);
        Task<Jugador> UpdateJugador(Jugador jugador);
        Task<Jugador> GetJugadorById(int jugadorId);
        Task<Jugador> DeleteJugadorById(int jugadorId);
        Task<Jugador> ToggleJugadorById(int jugadorId);
        Task<Jugador> ChangeJugadorTeam(Jugador jugador);
        Task<Jugador> HacerJugadorAgenteLibre(Jugador jugador);
    }
}
