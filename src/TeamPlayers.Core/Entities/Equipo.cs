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
        [MinLength(3)]
        [MaxLength(3)]
        public string Pais { get; set; } = default!;
        public int IdEstado { get; set; } = 1;
        public Estado Estado { get; set; } = default!;
        public ICollection<Jugador> Jugadores { get; set; }
    }
}
