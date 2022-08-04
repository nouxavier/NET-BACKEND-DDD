using ChallengeDominio;
using ChallengeDominio.Excecoes;
using ChallengeDominio.Model.Localidades.V1;
using ChallengeAplicacao.Repositorio;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ChallengeAplicacao.Repositorio.Localidade;

namespace ChallengeRepositorioPostgresql.Repositorio
{
    internal class RepositorioPais : IRepositorioPais
    {
        protected IContextoAplicacao Contexto { get; }
        protected IConfiguration Configuration { get; }
        protected ILogger Logger { get; }
        protected IStringLocalizer StringLocalizer { get; }

        protected readonly DbSet<Pais> _dbSet;
        private const int REGISTRO_POR_PAGINA_PADRAO = 100;

        public RepositorioPais(IContextoAplicacao contexto, 
            IConfiguration configuration, 
            ILogger logger, 
            IStringLocalizer stringLocalizer)
        {
            Contexto = contexto;
            Configuration = configuration;
            Logger = logger;
            StringLocalizer = stringLocalizer;
            _dbSet = Contexto.Instance.Set<Pais>();
        }

        public void Remove(Pais entidade)
        {
            Contexto.Paises.Remove(entidade);
        }

        public void Remove(Guid id)
        {
            Pais entidade = Carrega(id);
            if (entidade == null)
                throw new NaoEncontradoException(StringLocalizer["PaisNaoEncontrado"]);
            Remove(entidade);
        }

        public void Insere(Pais entidade)
        {
            Contexto.Paises.Add(entidade);
        }

        public void Atualiza(Pais entidade)
        {
            Contexto.Instance.Entry(entidade).State = EntityState.Modified;
        }

        public Pais Carrega(Guid id)
        {
            return Contexto.Paises.Find(id);
        }

        public IEnumerable<Pais> Pesquisa(OpcoesPesquisa opcoes)
        {
            var pr = PredicateBuilder.New<Pais>(true);

            int? registros = opcoes?.RegistrosPorPagina ?? REGISTRO_POR_PAGINA_PADRAO;
            return Contexto.Paises.Where(pr).Paginador(opcoes?.Pagina, registros);
        }
    }
}