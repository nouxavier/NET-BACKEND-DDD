using System;
using System.Collections.Generic;
using lo = ChallengeDominio.Model.Localidades.V1;


namespace ChallengeAplicacao.Operacoes
{
    public interface IOperacoesLocalidade
    {
        IEnumerable<lo.Localidade> CarregaCache();
    }
}
