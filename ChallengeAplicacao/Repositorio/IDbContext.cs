using Microsoft.EntityFrameworkCore;

namespace ChallengeAplicacao.Repositorio
{
    public interface IDbContext
    {
        DbContext Instance { get; }
    }
}
