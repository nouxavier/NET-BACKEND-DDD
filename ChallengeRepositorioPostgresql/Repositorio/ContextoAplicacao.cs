

using ChallengeDominio.Model.Localidades.V1;
using lo = ChallengeDominio.Model.Localidades.V1;
using ChallengeDominio.Model.Sensores.V1;
using Microsoft.EntityFrameworkCore;
using ChallengeAplicacao.Repositorio;

namespace ChallengeRepositorioPostgresql.Repositorio
{
    public class ContextoAplicacao : DbContext, IContextoAplicacao
    {
        public DbSet<lo.Localidade> Localidades { get; set; }
        public DbSet<Regiao> Regioes { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<EventoSensor> EventosSensores { get; set; }
        public DbContext Instance => this;

        public ContextoAplicacao(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<lo.Localidade>()
              .UseXminAsConcurrencyToken();
            modelBuilder.Entity<Regiao>()
              .UseXminAsConcurrencyToken();
            modelBuilder.Entity<Pais>()
              .UseXminAsConcurrencyToken();
            modelBuilder.Entity<Sensor>()
             .UseXminAsConcurrencyToken();
            modelBuilder.Entity<EventoSensor>()
             .UseXminAsConcurrencyToken();

            //n to n 

            modelBuilder.Entity<lo.Localidade>()
                .HasKey(l => l.Id);
            modelBuilder.Entity<lo.Localidade>()
                .HasOne(bc => bc.Pais)
                .WithMany(b => b.Localidades)
                .HasForeignKey(bc => bc.IdPais);
            modelBuilder.Entity<lo.Localidade>()
                .HasOne(bc => bc.Regiao)
                .WithMany(c => c.Localidades)
                .HasForeignKey(bc => bc.IdRegiao);

            //1 to n
            modelBuilder.Entity<lo.Localidade>()
              .HasMany(c => c.EventosSensores)
              .WithOne(e => e.Localidade)
              .HasForeignKey(c => c.IdLocalidade);
           modelBuilder.Entity<Sensor>()
              .HasMany(c => c.EventosSensores)
              .WithOne(e => e.Sensor)
              .HasForeignKey(c=>c.IdSensor);
            modelBuilder.Entity<lo.Localidade>()
              .HasMany(c => c.Sensores)
              .WithOne(e => e.Localidade)
              .HasForeignKey(c => c.IdLocalidade);



        }
    }
}
