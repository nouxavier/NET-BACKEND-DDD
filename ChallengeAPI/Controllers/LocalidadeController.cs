using ChallengeAplicacao.Operacoes;
using ChallengeDominio;
using ChallengeDominio.Cache.Localidade;
using ChallengeDominio.Model.Localidades.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using ChallengeDominio.VO;
using ChallengeDominio.VO.Localidades.V1;
using ChallengeDominio.Conversores.Localidades.V1;

namespace ChallengeAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class LocalidadeController: Controller
    {
        private ILogger Logger { get; }
        private IOperacoesLocalidade OperacaoLocalidade { get; }
        private IServiceProvider ServiceProvider { get; }
        private IStringLocalizer StringLocalizer { get; }
        private ICacheLocalidade CacheLocalidade { get; }

        public LocalidadeController(ILogger logger,
            IOperacoesLocalidade operacaoLocalidade,
            IServiceProvider serviceProvider,
            IStringLocalizer stringLocalizer,
            ICacheLocalidade cacheLocalidade)
        {
            Logger = logger;
            OperacaoLocalidade = operacaoLocalidade;
            ServiceProvider = serviceProvider;
            StringLocalizer = stringLocalizer;
            CacheLocalidade = cacheLocalidade;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<LocalidadeVO>> Get([FromQuery] OpcoesPesquisaVO opcoes)
        {
            try
            {
                var result = OperacaoLocalidade.CarregaCache();
                if (result == null )
                    return NotFound(StringLocalizer["LocalidadeNaoEncontrado"]);
                return Ok(new ConversorLocalidade().ListaConverte(result));
            }
            catch (Exception ex)
            {
                return this.CriaResultExcecao(ex);
            }
        }
    }
}
