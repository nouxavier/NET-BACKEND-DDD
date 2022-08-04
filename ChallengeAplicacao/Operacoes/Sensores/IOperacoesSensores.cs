using ChallengeDominio.Model.Sensores;
using ChallengeDominio.Model.Sensores.V1;
using System;
using System.Collections.Generic;


namespace ChallengeAplicacao.Operacoes.Sensores
{
    public interface IOperacoesSensores
    {
        IEnumerable<EventoSensor> CarregaEventosSensores(IOpcoesEventosSensores opcoes);
        EventoSensor CarregaEventosSensores(Guid id);
        void InsereEventosSensores(EventoSensor eventoSensor);

    }
}
