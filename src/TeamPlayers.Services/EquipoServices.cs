using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TeamPlayers.Core;
using TeamPlayers.Core.Entities;
using TeamPlayers.Core.Repositories;
using TeamPlayers.Core.Services;

namespace TeamPlayers.Services
{
    public class EquipoServices : IEquipoServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public EquipoServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Equipo> AddEquipo(Equipo equipo)
        {
            var errors = new List<ValidationResult>();
            if (!Validator.TryValidateObject(equipo, new ValidationContext(equipo), errors, true))
                throw new InvalidOperationException(string.Join(Environment.NewLine, errors.Select(x => x.ErrorMessage)));

            if (equipo == null)
                throw new ArgumentNullException(MessageHandler.E1);

            var addedEquipo = await _unitOfWork.Equipo.AddAsync(equipo);
            await _unitOfWork.SaveAsync();

            return addedEquipo;
        }

        public async Task<Equipo> DeleteEquipoById(int equipoId)
        {
            if (equipoId == 0)
                throw new ArgumentNullException(MessageHandler.E2);

            var dbEquipo = await _unitOfWork.Equipo.GetByIdAsync(equipoId);

            if (dbEquipo == null)
                throw new NullReferenceException(MessageHandler.E3);

            _unitOfWork.Equipo.RemoveById(equipoId);
            await _unitOfWork.SaveAsync();

            return dbEquipo;
        }

        public async Task<IEnumerable<Equipo>> GetAllEquipos()
        {
            var equipos = await _unitOfWork.Equipo.GetAllAsync(orderBy: x => x.OrderByDescending(x => x.Creado), includeProperties: "Estado");
            return equipos;
        }

        public async Task<IEnumerable<Equipo>> GetAllEquiposByStatus(TipoEstado estado)
        {
            var equipos = await _unitOfWork.Equipo.GetAllAsync((e) => e.IdEstado == (int)estado,
                orderBy: x => x.OrderByDescending(x => x.Creado), 
                includeProperties: "Estado");
            return equipos;
        }

        public async Task<Equipo> GetEquipoById(int equipoId)
        {
            if (equipoId == 0)
                throw new ArgumentNullException(MessageHandler.E2);

            var dbEquipo = await _unitOfWork.Equipo.GetByIdAsync(equipoId);

            if (dbEquipo == null)
                throw new NullReferenceException(MessageHandler.E3);

            return dbEquipo;
        }

        public async Task<Equipo> UpdateEquipo(Equipo equipo)
        {
            if (equipo == null)
                throw new ArgumentNullException(MessageHandler.E1);

            if (equipo.Id == 0)
                throw new ArgumentNullException(MessageHandler.E2);

            var updatedEquipo = await _unitOfWork.Equipo.UpdateAsync(equipo);

            if (updatedEquipo == null)
                throw new NullReferenceException(MessageHandler.E3);

            await _unitOfWork.SaveAsync();

            return updatedEquipo;
        }

        public async Task<Equipo> ToggleEquipoById(int equipoId)
        {
            if (equipoId == 0)
                throw new ArgumentNullException(MessageHandler.E2);

            var dbEquipo = await _unitOfWork.Equipo.GetByIdAsync(equipoId);

            var status = ((TipoEstado)dbEquipo.IdEstado == TipoEstado.Activo) ? TipoEstado.Inactivo : TipoEstado.Activo;

            if (dbEquipo == null)
                throw new NullReferenceException(MessageHandler.E3);

            var toggledEquipo = await _unitOfWork.Equipo.ChangeStatus(dbEquipo, status);

            if (toggledEquipo == null)
                throw new NullReferenceException(MessageHandler.E3);

            await _unitOfWork.SaveAsync();

            return toggledEquipo;
        }
    }
}
