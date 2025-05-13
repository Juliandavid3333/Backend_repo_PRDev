using Microsoft.EntityFrameworkCore;
using PruebaDev.Models;
using System;

namespace PruebaDev.BaseDatos
{
    public class ContextBD: DbContext
    {
        public ContextBD(DbContextOptions<ContextBD> options) : base(options) { }

        public DbSet<GifSearch> Historial { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GifSearch>(entity =>
            {
                entity.ToTable("historial");
            });
        }
    }
}
