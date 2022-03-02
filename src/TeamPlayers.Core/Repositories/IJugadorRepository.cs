using System.Threading.Tasks;
using TeamPlayers.Core.Entities;

namespace TeamPlayers.Core.Repositories
{
    public interface IJugadorRepository : IRepository<Jugador>
    {
        Task<Jugador> UpdateAsync(Jugador estado);
    }
}
