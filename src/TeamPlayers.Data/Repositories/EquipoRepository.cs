using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TeamPlayers.Core.Entities;
using TeamPlayers.Core.Repositories;

namespace TeamPlayers.Data.Repositories
{
    public class EquipoRepository : Repository<Equipo>, IEquipoRepository
    {
        private readonly TeamPlayersContext _db;

        public EquipoRepository(TeamPlayersContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Equipo> UpdateAsync(Equipo equipo)
        {
            var dbEquipo = await _db.Equipos.FirstOrDefaultAsync(c => c.Id == equipo.Id);

            if (dbEquipo != null)
            {
                dbEquipo!.Nombre = equipo.Nombre ?? dbEquipo.Nombre;
                dbEquipo.Pais = equipo.Pais ?? dbEquipo.Pais;
            }

            return dbEquipo!;
        }

        public async Task<Equipo> ChangeStatus(Equipo equipo, Estados estado)
        {
            var dbEquipo = await _db.Equipos.FirstOrDefaultAsync(c => c.Id == equipo.Id);

            if(dbEquipo != null)
                dbEquipo!.IdEstado = (int)estado;

            return dbEquipo!;
        }
    }
}
