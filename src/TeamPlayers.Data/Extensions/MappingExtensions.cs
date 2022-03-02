using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TeamPlayers.Core.Entities;

namespace System
{
    public static class MappingExtensions
    {
        public static ModelBuilder AddEstadoMapping(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(150)
                .HasConversion(e => e.ToUpper(), e => e.ToUpper())
                .IsUnicode(false);

                entity.Property(e => e.Tipo)
                .IsRequired();

                entity.Property(e => e.Creado).ValueGeneratedOnAdd();
            });

            return modelBuilder;
        }

        public static ModelBuilder AddEquipoMapping(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipo>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(250)
                .HasConversion(e => e.ToUpper(), e => e.ToUpper())
                .IsUnicode(false);

                entity.HasIndex(e => e.Nombre)
                .IsUnique();

                entity.Property(e => e.Pais)
                .IsRequired()
                .HasMaxLength(3)
                .HasConversion(e => e.ToUpper(), e => e.ToUpper())
                .IsUnicode(false);

                entity.HasOne(e => e.Estado)
                .WithMany()
                .HasForeignKey(e => e.IdEstado)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(e => e.Jugadores)
                .WithOne(j => j.Equipo!)
                .HasForeignKey(j => j.IdEquipo)
                .OnDelete(DeleteBehavior.SetNull);

                entity.Property(e => e.Creado).ValueGeneratedOnAdd();
            });

            return modelBuilder;
        }

        public static ModelBuilder AddJugadorMapping(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jugador>(entity =>
            {
                entity.HasKey(j => j.Id);

                entity.Property(j => j.Nombre)
                .IsRequired()
                .HasMaxLength(250)
                .HasConversion(j => j.ToUpper(), j => j.ToUpper())
                .IsUnicode(false); //para forzar el varchar en la base de datos

                entity.Property(j => j.Apellido)
                .IsRequired()
                .HasMaxLength(250)
                .HasConversion(j => j.ToUpper(), j => j.ToUpper())
                .IsUnicode(false);

                entity.Property(j => j.Nacimiento)
                .IsRequired();

                entity.Property(j => j.Pasaporte)
                .IsRequired()
                .HasMaxLength(9)
                .HasConversion(j => j.ToUpper(), j => j.ToUpper())
                .IsUnicode(false);

                entity.HasIndex(j => j.Pasaporte)
                .IsUnique();

                entity.Property(j => j.Direccion)
                .IsRequired()
                .HasMaxLength(500)
                .HasConversion(j => j.ToUpper(), j => j.ToUpper())
                .IsUnicode(false);

                entity.Property(j => j.Sexo)
                .IsRequired();

                entity.HasOne(e => e.Estado)
                 .WithMany()
                 .HasForeignKey(e => e.IdEstado)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.NoAction);

                entity.Property(j => j.IdEquipo) //en caso de que el jugador sea agente libre no deberia tener equipo
                .IsRequired(false);

                entity.HasOne(j => j.Equipo)
                .WithMany(e => e!.Jugadores)
                .HasForeignKey(j => j.IdEquipo);

                entity.Property(e => e.Creado).ValueGeneratedOnAdd();
            });

            return modelBuilder;
        }
    }
}
