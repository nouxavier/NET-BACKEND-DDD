using ChallengeDominio.Model.Sensores.V1;
using Microsoft.EntityFrameworkCore;
using lo = ChallengeDominio.Model.Localidades.V1;

namespace ChallengeAplicacao.Repositorio
{
    public interface IContextoAplicacao : IDbContext 
    {
        DbSet<lo.Localidade> Localidades { get; set; }
        DbSet<lo.Regiao> Regioes { get; set; }
        DbSet<lo.Pais> Paises { get; set; }
        DbSet<Sensor> Sensores { get; set; }
        DbSet<EventoSensor> EventosSensores { get; set; }
    }
}
