using System.Collections.Generic;
using lo = ChallengeDominio.Model.Localidades.V1;

namespace ChallengeDominio.Cache.Localidade
{
    public interface ICacheLocalidade: ICache<IEnumerable<lo.Localidade>>
    {
    }
}
