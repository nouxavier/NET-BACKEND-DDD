using ChallengeAplicacao.Operacoes;
using ChallengeAplicacao.Operacoes.Sensores;
using ChallengeDominio.Cache.Localidade;
using ChallengeDominio.Conversores;
using ChallengeDominio.Conversores.Sensores.V1;
using ChallengeDominio.Model.Sensores;
using ChallengeDominio.Model.Sensores.V1;
using ChallengeDominio.VO.EventoSensor.V1;
using ChallengeDominio.VO.Sensores.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class EventoSensorController : Controller
    {
        private ILogger Logger { get; }
        private IOperacoesSensores OperacaoSensores { get; }
        private IServiceProvider ServiceProvider { get; }
        private IStringLocalizer StringLocalizer { get; }
        private ICacheLocalidade CacheLocalidade { get; }

        public EventoSensorController(ILogger logger,
            IOperacoesSensores operacaoSensores,
            IOperacoesLocalidade operacoesLocalidade,
            IServiceProvider serviceProvider,
            IStringLocalizer stringLocalizer,
            ICacheLocalidade cacheLocalidade)
        {
            Logger = logger;
            OperacaoSensores = operacaoSensores;
            ServiceProvider = serviceProvider;
            StringLocalizer = stringLocalizer;
            CacheLocalidade = cacheLocalidade;
            operacoesLocalidade.CarregaCache();

        }
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EventoSensorVO> Get(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid idG)) return NotFound(StringLocalizer["EventoSensorNaoEncontrado"]);
                var eventoSensor = OperacaoSensores.CarregaEventosSensores(idG);
                if (eventoSensor == null) return NotFound(StringLocalizer["EventoSensorNaoEncontrado"]);
                return Ok(new ConversorEventoSensor(CacheLocalidade, StringLocalizer).Converte(eventoSensor));
            }
            catch (Exception ex) { return this.CriaResultExcecao(ex); }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<EventoSensorVO>> Get([FromQuery] OpcoesEventosSensoresVO opcoes)
        {
            try
            {

                IConversor<IOpcoesEventosSensores, OpcoesEventosSensoresVO> conversor =
                    (IConversor<IOpcoesEventosSensores, OpcoesEventosSensoresVO>)ServiceProvider
                    .GetService(typeof(IConversor<IOpcoesEventosSensores, OpcoesEventosSensoresVO>));
                var result = OperacaoSensores.CarregaEventosSensores(conversor.Converte(opcoes));
                if (result == null || result.Count() < 1)
                    return NotFound(StringLocalizer["EventoSensorNaoEncontrado"]);

                return Ok(new ConversorEventoSensor(CacheLocalidade, StringLocalizer).ListaConverte(result));
            }
            catch (Exception ex)
            {
                return this.CriaResultExcecao(ex);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EventoSensorVO> Post([FromBody] EventoSensorVO evento)
        {
            try
            {
                if (evento == null) return BadRequest(StringLocalizer["EventoSensorNaoEncontrado"]);
                evento.Id = Guid.NewGuid();
                OperacaoSensores.InsereEventosSensores(new ConversorEventoSensor(CacheLocalidade, StringLocalizer).Converte(evento));
                return Ok(evento);
            }
            catch (Exception ex) 
            {
                return this.CriaResultExcecao(ex); 
            }
        }

    }

}
