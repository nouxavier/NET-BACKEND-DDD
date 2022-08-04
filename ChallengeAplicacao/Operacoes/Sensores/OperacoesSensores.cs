using ChallengeAplicacao.Repositorio;
using ChallengeDominio._Util;
using ChallengeDominio.Model.Sensores;
using ChallengeDominio.Model.Sensores.V1;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using ChallengeDominio.Excecoes;

namespace ChallengeAplicacao.Operacoes.Sensores
{
    public class OperacoesSensores : IOperacoesSensores
    {
        private ILogger Logger { get; }
        private IServiceProvider Provider { get; }
        private IStringLocalizer StringLocalizer { get; }
        private ITradutorExcecao TradutorExcecao { get; }

        public OperacoesSensores(IServiceProvider provider, ILogger logger, IStringLocalizer stringLocalizer, ITradutorExcecao tradutorExcecao)
        {
            Provider = provider;
            Logger = logger;
            StringLocalizer = stringLocalizer;
            TradutorExcecao = tradutorExcecao;
        }

        #region Utils
        private IUoWAplicacao CriaUoWAplicacao() =>
            (IUoWAplicacao)Provider.GetService<IUoWAplicacao>();
        #endregion
        public IEnumerable<EventoSensor> CarregaEventosSensores(IOpcoesEventosSensores opcoes)
        {
            var work = CriaUoWAplicacao();
            return work.RepositorioEventoSensor.Pesquisa((OpcoesEventosSensores)opcoes);
            
        }
        public EventoSensor CarregaEventosSensores(Guid id)
        {
            throw new NotImplementedException();
        }

        public void InsereEventosSensores(EventoSensor eventoSensor)
        {
            try
            {
                eventoSensor.Valida(StringLocalizer);

                var work = CriaUoWAplicacao();
                work.RepositorioEventoSensor.Insere(eventoSensor);
                work.Commit();
            }
            catch (Exception ex)
            {
                var excecao = TradutorExcecao.Converte(ex);
                switch (excecao)
                {
                    case DuplicadoException e:
                        throw new DuplicadoException(StringLocalizer["EventoSensor.Duplicado"], e.InnerException);
                    case ReferenciaIncorretaException e:
                        throw e.Message switch
                        {
                            "eventos_sensores_sensor_fk1" => new ReferenciaIncorretaException(StringLocalizer["EventoSensor.Localidade"], e.InnerException),
                            "eventos_localidade_sensor_fk1" => new ReferenciaIncorretaException(StringLocalizer["EventoSensor.Sensor"], e.InnerException),
                            _ => new ReferenciaIncorretaException(ex.Message, e.InnerException)
                        };
                    default: throw;
                }
            }
        }
       
    }
}
