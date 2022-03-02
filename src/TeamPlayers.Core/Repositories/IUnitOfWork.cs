using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TeamPlayers.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IEstadoRepository Estado { get; }
        IEquipoRepository Equipo { get; }
        IJugadorRepository Jugador { get;  }
        Task SaveAsync();
    }
}
