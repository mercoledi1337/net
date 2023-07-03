using Microsoft.EntityFrameworkCore;
using Przychodnia.Models;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Metadata;

namespace Przychodnia.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Termin> Terminy { get; set; }
        public DbSet<LekRecepta> LekiRecepty { get; set; }
        public DbSet<Lek> Leki { get; set; }
        public DbSet<Lekarz> Lekarze { get; set; }
        public DbSet<Pacjent> Pacjenci { get; set;}
        public DbSet<Recepta> Recepty { get; set; }
        public DbSet<Uzytkownik> Uzytkownicy { get; set; }
        public DbSet<Wizyta> Wizyty { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<LekRecepta>()
               .HasKey(lr => new { lr.LekId, lr.ReceptaId });
            modelBuilder.Entity<LekRecepta>()
                .HasOne(l => l.Lek)
                .WithMany(lr => lr.LekiRecepty)
                .HasForeignKey(l => l.LekId);
            modelBuilder.Entity<LekRecepta>()
                .HasOne(l => l.Recepta)
                .WithMany(lr => lr.LekiRecepty)
                .HasForeignKey(ls => ls.ReceptaId);

            {
                modelBuilder.Entity<Wizyta>()
                    .HasOne(w => w.Termin)
                    .WithMany()
                    .HasForeignKey(w => w.TerminId)
                    .OnDelete(DeleteBehavior.NoAction);
            }



        }
    }
}
