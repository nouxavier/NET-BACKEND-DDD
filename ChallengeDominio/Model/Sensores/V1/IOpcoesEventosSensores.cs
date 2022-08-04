

namespace ChallengeDominio.Model.Sensores
{
    public interface IOpcoesEventosSensores: IOpcoesPesquisa
    {
        string Pais { get; set; }
        string Regiao { get; set; }
        string Sensor { get; set; }
    }
}
