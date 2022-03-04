using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TeamPlayers.Core.Entities;
using TeamPlayers.Core.Repositories;

namespace TeamPlayers.Data.Repositories
{
    public class JugadorRepository : Repository<Jugador>, IJugadorRepository
    {
        private readonly TeamPlayersContext _db;

        public JugadorRepository(TeamPlayersContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Jugador> UpdateAsync(Jugador jugador)
        {
            var dbJugador = await _db.Jugadores.FirstOrDefaultAsync(c => c.Id == jugador.Id);

            if (dbJugador != null)
            {
                dbJugador!.Nombre = jugador.Nombre ?? dbJugador.Nombre;
                dbJugador!.Apellido = jugador.Apellido ?? dbJugador.Apellido;
                dbJugador!.Nacimiento = jugador.Nacimiento;
                dbJugador!.Pasaporte = jugador.Pasaporte ?? dbJugador.Pasaporte;
                dbJugador!.Direccion = jugador.Direccion ?? dbJugador.Direccion;
                dbJugador!.Sexo = jugador.Sexo;
            }

            return dbJugador!;
        }

        public async Task<Jugador> ChangeStatus(Jugador jugador, TipoEstado estado)
        {
            var dbJugador = await _db.Jugadores.FirstOrDefaultAsync(c => c.Id == jugador.Id);

            if (dbJugador != null)
                dbJugador!.IdEstado = (int)estado;

            return dbJugador!;
        }

        public async Task<Jugador> AssignTeam(Jugador jugador)
        {
            var dbJugador = await _db.Jugadores.FirstOrDefaultAsync(c => c.Id == jugador.Id);

            if (dbJugador != null)
                dbJugador!.IdEquipo = jugador.IdEquipo;

            return dbJugador!;
        }
    }
}
