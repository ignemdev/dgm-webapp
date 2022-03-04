using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeamPlayers.Core.Entities
{
    public class Equipo : BaseEntity
    {
        public Equipo()
        {
            Jugadores = new Collection<Jugador>();
        }
        
        [Required(AllowEmptyStrings = false)]
        [MaxLength(3)]
        [MinLength(3)]
        public string Pais { get; set; } = default!;
        public int IdEstado { get; set; } = (int)Estados.Activo;
        public Estado Estado { get; set; } = default!;
        public ICollection<Jugador> Jugadores { get; set; }
    }
}
