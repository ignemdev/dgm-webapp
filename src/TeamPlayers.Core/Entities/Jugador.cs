using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeamPlayers.Core.Entities
{
    public class Jugador : BaseEntity
    {
        [Required(AllowEmptyStrings = false)]
        public string Apellido { get; set; } = default!;
        [DataType(DataType.DateTime)]
        public DateTime Nacimiento { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(9)]
        public string Pasaporte { get; set; } = default!;
        [Required(AllowEmptyStrings = false)]
        public string Direccion { get; set; } = default!;
        public char Sexo { get; set; } = default!;
        public int? IdEquipo { get; set; }
        public int IdEstado { get; set; }
        public Estado Estado { get; set; } = default!;
        public Equipo? Equipo { get; set; } = default!;
    }
}
