using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPlayers.Core;
using TeamPlayers.Core.Entities;
using TeamPlayers.Core.Repositories;
using TeamPlayers.Core.Services;

namespace TeamPlayers.Services
{
    public class EstadoServices : IEstadoServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public EstadoServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Estado> AddEstado(Estado estado)
        {
            var errors = new List<ValidationResult>();
            if (!Validator.TryValidateObject(estado, new ValidationContext(estado), errors, true))
                throw new InvalidOperationException(string.Join(Environment.NewLine, errors.Select(x => x.ErrorMessage)));

            if (estado == null)
                throw new ArgumentNullException(MessageHandler.E1);

            var addedEstado = await _unitOfWork.Estado.AddAsync(estado);
            await _unitOfWork.SaveAsync();

            return addedEstado;
        }

        public async Task DeleteEstadoById(int estadoId)
        {
            if (estadoId == 0)
                throw new ArgumentNullException(MessageHandler.E2);

            var dbEstado = await _unitOfWork.Estado.GetByIdAsync(estadoId);

            if (dbEstado == null)
                throw new NullReferenceException(MessageHandler.E3);

            _unitOfWork.Estado.RemoveById(estadoId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Estado>> GetAllEstados()
        {
            var estados = await _unitOfWork.Estado.GetAllAsync(orderBy: x => x.OrderByDescending(x => x.Creado));
            return estados;
        }

        public async Task<Estado> GetEstadoById(int estadoId)
        {
            if (estadoId == 0)
                throw new ArgumentNullException(MessageHandler.E2);

            var dbEstado = await _unitOfWork.Estado.GetByIdAsync(estadoId);

            if (dbEstado == null)
                throw new NullReferenceException(MessageHandler.E3);

            return dbEstado;
        }

        public async Task<Estado> UpdateEstado(Estado estado)
        {
            if (estado == null)
                throw new ArgumentNullException(MessageHandler.E1);

            if (estado.Id == 0)
                throw new ArgumentNullException(MessageHandler.E2);

            var updatedEstado = await _unitOfWork.Estado.UpdateAsync(estado);

            if (updatedEstado == null)
                throw new NullReferenceException(MessageHandler.E3);

            await _unitOfWork.SaveAsync();

            return updatedEstado;
        }
    }
}
