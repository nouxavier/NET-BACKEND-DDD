using System.Linq;

namespace ChallengeAplicacao.Repositorio
{
    public static class ExtensaoPaginador
    {
        const int LIMITE_REGISTROS_PADRAO = 100;
        public static IQueryable<T> Paginador<T>(this IQueryable<T> consulta, int? pagina, int? registrosPorPagina)
        {
            int nroPagina = pagina.GetValueOrDefault(1);
            int registros = registrosPorPagina.GetValueOrDefault(LIMITE_REGISTROS_PADRAO);

            return consulta.Skip((nroPagina - 1) * registros).Take(registros);
        }
    }
}
