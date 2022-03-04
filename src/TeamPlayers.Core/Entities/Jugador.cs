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
        [DataType(DataType.Date)]
        public DateTime Nacimiento { get; set; } = DateTime.Now;

        [Required(AllowEmptyStrings = false)]
        [MaxLength(9)]
        [MinLength(9)]
        public string Pasaporte { get; set; } = default!;
        [Required(AllowEmptyStrings = false)]
        public string Direccion { get; set; } = default!;
        public char Sexo { get; set; } = default!;
        [Display(Name = "Equipo")]
        public int? IdEquipo { get; set; }
        public int IdEstado { get; set; } = (int)Estados.AgenteLibre;
        public Estado Estado { get; set; } = default!;
        public Equipo? Equipo { get; set; } = default!;
    }
}
