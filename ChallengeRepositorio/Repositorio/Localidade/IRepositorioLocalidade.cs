

using ChallengeDominio;
using lo = ChallengeDominio.Model.Localidades.V1;

namespace ChallengeRepositorio.Repositorio
{
    public interface IRepositorioLocalidade : IRepositorioBase<lo.Localidade, OpcoesPesquisa>
    {
    }
}
