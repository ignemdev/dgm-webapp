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
    public class JugadorServices : IJugadorServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public JugadorServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Jugador> AddJugador(Jugador jugador)
        {
            var errors = new List<ValidationResult>();
            if (!Validator.TryValidateObject(jugador, new ValidationContext(jugador), errors, true))
                throw new InvalidOperationException(string.Join(Environment.NewLine, errors.Select(x => x.ErrorMessage)));

            if (jugador == null)
                throw new ArgumentNullException(MessageHandler.E1);

            var addedJugador = await _unitOfWork.Jugador.AddAsync(jugador);
            await _unitOfWork.SaveAsync();

            return addedJugador;
        }

        public async Task DeleteJugadorById(int jugadorId)
        {
            if (jugadorId == 0)
                throw new ArgumentNullException(MessageHandler.E2);

            var dbJugador = await _unitOfWork.Jugador.GetByIdAsync(jugadorId);

            if (dbJugador == null)
                throw new NullReferenceException(MessageHandler.E3);

            _unitOfWork.Jugador.RemoveById(jugadorId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Jugador>> GetAllJugadores()
        {
            var jugadors = await _unitOfWork.Jugador.GetAllAsync(orderBy: x => x.OrderByDescending(x => x.Creado));
            return jugadors;
        }

        public async Task<Jugador> GetJugadorById(int jugadorId)
        {
            if (jugadorId == 0)
                throw new ArgumentNullException(MessageHandler.E2);

            var dbJugador = await _unitOfWork.Jugador.GetByIdAsync(jugadorId);

            if (dbJugador == null)
                throw new NullReferenceException(MessageHandler.E3);

            return dbJugador;
        }

        public async Task<Jugador> UpdateJugador(Jugador jugador)
        {
            if (jugador == null)
                throw new ArgumentNullException(MessageHandler.E1);

            if (jugador.Id == 0)
                throw new ArgumentNullException(MessageHandler.E2);

            var updatedJugador = await _unitOfWork.Jugador.UpdateAsync(jugador);

            if (updatedJugador == null)
                throw new NullReferenceException(MessageHandler.E3);

            await _unitOfWork.SaveAsync();

            return updatedJugador;
        }
    }
}
