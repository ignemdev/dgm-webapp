using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeamPlayers.Core.Entities;

namespace TeamPlayers.Data
{
    public class TeamPlayersContext : DbContext
    {
        public TeamPlayersContext(DbContextOptions<TeamPlayersContext> options) : base(options) { }

        public DbSet<Estado> Estados { get; set; } = default!;
        public DbSet<Equipo> Equipos { get; set; } = default!;
        public DbSet<Jugador> Jugadores { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEstadoMapping();
            modelBuilder.AddEquipoMapping();
            modelBuilder.AddJugadorMapping();

            base.OnModelCreating(modelBuilder);
        }
    }
}
