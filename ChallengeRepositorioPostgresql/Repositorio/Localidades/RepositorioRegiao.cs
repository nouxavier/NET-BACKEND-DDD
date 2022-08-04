using ChallengeAplicacao.Repositorio;
using ChallengeAplicacao.Repositorio.Localidade;
using ChallengeDominio;
using ChallengeDominio.Excecoes;
using ChallengeDominio.Model.Localidades.V1;
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
    internal class RepositorioRegiao : IRepositorioRegiao
    {
        protected IContextoAplicacao Contexto { get; }
        protected IConfiguration Configuration { get; }
        protected ILogger Logger { get; }
        protected IStringLocalizer StringLocalizer { get; }
        protected readonly DbSet<Regiao> _dbSet;
        private const int REGISTRO_POR_PAGINA_PADRAO = 100;
        public RepositorioRegiao(IContextoAplicacao contexto, 
            IConfiguration configuration, 
            ILogger logger, 
            IStringLocalizer stringLocalizer)
        {
            Contexto = contexto;
            Configuration = configuration;
            Logger = logger;
            StringLocalizer = stringLocalizer;
            _dbSet = Contexto.Instance.Set<Regiao>();
        }

        public void Remove(Regiao entidade)
        {
            Contexto.Regioes.Remove(entidade);
        }

        public void Remove(Guid id)
        {
            Regiao entidade = Carrega(id);
            if (entidade == null)
                throw new NaoEncontradoException(StringLocalizer["RegiaoNaoEncontrado"]);
            Remove(entidade);
        }

        public void Insere(Regiao entidade)
        {
            Contexto.Regioes.Add(entidade);
        }

        public void Atualiza(Regiao entidade)
        {
            Contexto.Instance.Entry(entidade).State = EntityState.Modified;
        }

        public Regiao Carrega(Guid id)
        {
            return Contexto.Regioes.Find(id);
        }

        public IEnumerable<Regiao> Pesquisa(OpcoesPesquisa opcoes)
        {
            var pr = PredicateBuilder.New<Regiao>(true);

            int? registros = opcoes?.RegistrosPorPagina ?? REGISTRO_POR_PAGINA_PADRAO;
            return Contexto.Regioes.Where(pr).Paginador(opcoes?.Pagina, registros);
        }
    }
}