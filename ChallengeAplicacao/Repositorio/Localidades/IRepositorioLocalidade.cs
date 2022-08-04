

using ChallengeDominio;
using System.Collections.Generic;
using lo = ChallengeDominio.Model.Localidades.V1;

namespace ChallengeAplicacao.Repositorio
{
    public interface IRepositorioLocalidade : IRepositorioBase<lo.Localidade, OpcoesPesquisa>
    {
        IEnumerable<lo.Localidade> PesquisaTodos(OpcoesPesquisa opcoes);
    }
}
