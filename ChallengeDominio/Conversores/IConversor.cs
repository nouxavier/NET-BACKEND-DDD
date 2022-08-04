using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeDominio.Conversores
{
    public interface IConversor<O, D>
    {
        D Converte(O origem);
        O Converte(D origem);
       
        void CopiaPropriedades(O origem, D destino);
        void CopiaPropriedades(D origem, O destino);
    }
}
