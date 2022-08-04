using Microsoft.Extensions.Localization;

namespace ChallengeDominio.Model.Sensores.V1
{
    public class OpcoesEventosSensores : OpcoesPesquisa, IOpcoesEventosSensores
    {
        public string Pais { get; set; }
        public string Regiao { get; set; }
        public string Sensor { get; set; }
        public OpcoesEventosSensores(IStringLocalizer stringLocalizer) : base(stringLocalizer)
        {

        }
    }
}
