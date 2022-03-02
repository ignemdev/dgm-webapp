using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamPlayers.Core.Entities;
using TeamPlayers.Core.Repositories;

namespace TeamPlayers.Data.Repositories
{
    public class EstadoRepository : Repository<Estado>, IEstadoRepository
    {
        private readonly TeamPlayersContext _db;
        public EstadoRepository(TeamPlayersContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Estado> UpdateAsync(Estado estado)
        {
            var dbEstado = await _db.Estados.FirstOrDefaultAsync(c => c.Id == estado.Id);

            if (dbEstado != null)
            {
                dbEstado!.Nombre = estado.Nombre ?? dbEstado.Nombre;
                dbEstado.Tipo = estado.Tipo;
            }

            return dbEstado!;
        }
    }
}
