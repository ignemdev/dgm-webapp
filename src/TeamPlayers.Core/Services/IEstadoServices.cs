using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamPlayers.Core.Entities;

namespace TeamPlayers.Core.Services
{
    public interface IEstadoServices
    {
        Task<IEnumerable<Estado>> GetAllEstados();
        Task<Estado> AddEstado(Estado estado);
        Task<Estado> UpdateEstado(Estado estado);
        Task<Estado> GetEstadoById(int estadoId);
        Task DeleteEstadoById(int estadoId);
    }
}
