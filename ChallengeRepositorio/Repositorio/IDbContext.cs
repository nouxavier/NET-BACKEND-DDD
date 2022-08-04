using Microsoft.EntityFrameworkCore;

namespace ChallengeRepositorio.Repositorio
{
    public interface IDbContext
    {
        DbContext Instance { get; }
    }
}
