using ChallengeAplicacao.Repositorio;
using ChallengeDominio;
using ChallengeDominio._Util;
using ChallengeDominio.Excecoes;
using ChallengeDominio.Model.Localidades.V1;
using ChallengeDominio.Model.Sensores.V1;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChallengeRepositorioPostgresql.Repositorio
{
    public class RepositorioEventoSensor : IRepositorioEventoSensor
    {
        private const int REGISTRO_POR_PAGINA_PADRAO = 100;
        protected IContextoAplicacao Contexto { get; }
        protected IConfiguration Configuration { get; }
        protected ILogger Logger { get; }
        protected IStringLocalizer StringLocalizer { get; }

        public RepositorioEventoSensor(IContextoAplicacao contexto,
            IConfiguration configuration,
            ILogger logger,
            IStringLocalizer stringLocalizer)
        {
            Contexto = contexto;
            Configuration = configuration;
            Logger = logger;
            StringLocalizer = stringLocalizer;
        }
        public void Atualiza(EventoSensor entidade)
        {
            Contexto.Instance.Entry(entidade).State = EntityState.Modified;
        }

        public EventoSensor Carrega(Guid id)
        {
            return Contexto.EventosSensores.Find(id);
        }

        public void Insere(EventoSensor entidade)
        {
            Contexto.EventosSensores.Add(entidade);
        }

        public IEnumerable<EventoSensor> Pesquisa(OpcoesEventosSensores opcoes)
        {
            var ps = PredicateBuilder.New<Sensor>(true);
            var pr = PredicateBuilder.New<Regiao>(true);
            var pp = PredicateBuilder.New<Pais>(true);

            if (opcoes != null)
            {
                opcoes.Valida();
                if (!string.IsNullOrWhiteSpace(opcoes.Sensor))
                    ps.And(p => EF.Functions.Like(p.Nome, AjustaLike(Transformador.Normaliza(opcoes.Sensor))));
                if (!string.IsNullOrWhiteSpace(opcoes.Regiao))
                    pr.And(p => EF.Functions.Like(p.Nome, AjustaLike(Transformador.Normaliza(opcoes.Regiao))));
                if (!string.IsNullOrWhiteSpace(opcoes.Pais))
                    pp.And(p => EF.Functions.Like(p.Nome, AjustaLike(Transformador.Normaliza(opcoes.Pais))));
            }

            int? registros = opcoes?.RegistrosPorPagina ?? REGISTRO_POR_PAGINA_PADRAO;

            var query = (from eventosSensores in Contexto.EventosSensores
                         join sensor in Contexto.Sensores.Where(ps) on eventosSensores.IdSensor equals sensor.Id
                         select eventosSensores).ToList();
            return query;
        }
        private string AjustaLike(string termo)
        {
            termo = termo.Replace(" ", "%");
            if (!termo.EndsWith("%"))
                termo += "%";
            if (!termo.StartsWith("%"))
                termo = "%" + termo;

            return termo;
        }
        public void Remove(EventoSensor entidade)
        {
            Contexto.EventosSensores.Remove(entidade);
        }

        public void Remove(Guid id)
        {
            EventoSensor entidade = Carrega(id);
            if (entidade == null)
                throw new NaoEncontradoException(StringLocalizer["EventoSensorNaoEncontrado"]);
            Remove(entidade);
        }
    }
}
