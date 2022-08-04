using ChallengeDominio.Excecoes;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeDominio._Util
{
    public class Validador
    {
        private IStringLocalizer StringLocalizer { get; }

        public Validador(IStringLocalizer stringLocalizer)
        {
            StringLocalizer = stringLocalizer;
        }
        public void Obrigatorio(object valor, string propriedade)
        {
            if (valor == null)
                throw new ValidacaoException($"{StringLocalizer[propriedade]} {StringLocalizer["PropriedadeNaoPodeSerNula"]}");
        }
        public void ValidaRegra(bool regraInvalida, string msg)
        {
            if (regraInvalida)
                throw new ValidacaoException(StringLocalizer[msg]);
        }

        public void Intervalo(int? valor, string propriedade, int? minimo, int? maximo)
        {
            if (valor != null)
            {
                if (minimo.HasValue && valor < minimo.Value)
                    throw new ValidacaoException($"{StringLocalizer[propriedade]} {StringLocalizer["PropriedadeValorMenorQue"]} {minimo.Value}");
                if (maximo.HasValue && valor > maximo.Value)
                    throw new ValidacaoException($"{StringLocalizer[propriedade]} {StringLocalizer["PropriedadeValorMaiorQue"]} {maximo.Value}");
            }
        }
    }
}
