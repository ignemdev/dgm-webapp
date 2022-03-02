using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamPlayers.Core.Repositories;
using TeamPlayers.Data.Repositories;

namespace TeamPlayers.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TeamPlayersContext _db;

        public UnitOfWork(TeamPlayersContext db) 
        {
            _db = db;
            Estado = new EstadoRepository(_db);
            Equipo = new EquipoRepository(_db);
            Jugador = new JugadorRepository(_db);
        }

        public IEstadoRepository Estado { get; private set; }
        public IEquipoRepository Equipo { get; private set; }
        public IJugadorRepository Jugador { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
