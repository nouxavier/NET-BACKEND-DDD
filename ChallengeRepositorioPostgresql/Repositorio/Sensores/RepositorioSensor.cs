using ChallengeAplicacao.Repositorio;
using ChallengeDominio;
using ChallengeDominio.Excecoes;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using se = ChallengeDominio.Model.Sensores.V1;


namespace ChallengeRepositorioPostgresql.Repositorio
{
    public class RepositorioSensor : IRepositorioSensor
    {
        protected IContextoAplicacao Contexto { get; }
        protected IConfiguration Configuration { get; }
        protected ILogger Logger { get; }
        protected IStringLocalizer StringLocalizer { get; }

        private const int REGISTRO_POR_PAGINA_PADRAO = 100;
        public RepositorioSensor(IContextoAplicacao contexto,
            IConfiguration configuration,
            ILogger logger,
            IStringLocalizer stringLocalizer)
        {
            Contexto = contexto;
            Configuration = configuration;
            Logger = logger;
            StringLocalizer = stringLocalizer;
        }

        public void Remove(se.Sensor entidade)
        {
            Contexto.Sensores.Remove(entidade);
        }

        public void Remove(Guid id)
        {
            se.Sensor entidade = Carrega(id);
            if (entidade == null)
                throw new NaoEncontradoException(StringLocalizer["SensorNaoEncontrado"]);
            Remove(entidade);
        }

        public void Insere(se.Sensor entidade)
        {
            Contexto.Sensores.Add(entidade);
        }

        public void Atualiza(se.Sensor entidade)
        {
            Contexto.Instance.Entry(entidade).State = EntityState.Modified;
        }

        public se.Sensor Carrega(Guid id)
        {
            return Contexto.Sensores.Find(id);
        }

        public IEnumerable<se.Sensor> Pesquisa(OpcoesPesquisa opcoes)
        {
            var pr = PredicateBuilder.New<se.Sensor>(true);

            int? registros = opcoes?.RegistrosPorPagina ?? REGISTRO_POR_PAGINA_PADRAO;
            return Contexto.Sensores.Where(pr).Paginador(opcoes?.Pagina, registros);
        }
    }
}
