using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeamPlayers.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings =false)]
        public string Nombre { get; set; } = default!;
        public DateTime Creado { get; set; }
    }
}
