using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamPlayers.Core.Entities;

namespace TeamPlayers.Core.Repositories
{
    public interface IEstadoRepository : IRepository<Estado>
    {
        Task<Estado> UpdateAsync(Estado estado);
    }
}
