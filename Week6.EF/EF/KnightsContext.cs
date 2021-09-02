using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week6.EF.Core.Models;

namespace Week6.EF.EF
{
    public class KnightsContext : DbContext
    {
        //Proprietà di tipo DbSet (una per ogni entità che voglio mappare sul db)
        public DbSet<Knight> Knights { get; set; } //La tabella sul db si chiamerà Knights
                                                   //(se non faccio mapping tramite convenzioni)
        public DbSet<Weapon> Weapons { get; set; }

        public DbSet<Battle> Battles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;
		Database=KnightsDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Knight>()
                .HasMany(k => k.Battles)
                .WithMany(b => b.Knights)
                .UsingEntity<BattleKnight>(bk => bk.HasOne<Battle>().WithMany(),
                bk => bk.HasOne<Knight>().WithMany())
              //.ToTable("XYZBattleKnight") - per cambiare nome della tabella con il nome che passo
                .Property(bk => bk.DateJoined);
            //.HasDefaultValue(DateTime.Now);


            //per mappare su una colonna - cambiare nome della colonna
            //modelBuilder.Entity<BattleKnight>()
            //    .Property(bk => bk.KnightId)
            //    .HasColumnName("KnightsId");
            //modelBuilder.Entity<BattleKnight>()
            //    .Property(bk => bk.BattleId)
            //    .HasColumnName("BattlesBattleId");
        }
    }
}
