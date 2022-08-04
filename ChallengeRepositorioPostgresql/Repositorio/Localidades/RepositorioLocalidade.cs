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
using lo = ChallengeDominio.Model.Localidades.V1;

namespace ChallengeRepositorioPostgresql.Repositorio
{
    internal class RepositorioLocalidade : IRepositorioLocalidade
    {
        protected IContextoAplicacao Contexto { get; }
        protected IConfiguration Configuration { get; }
        protected ILogger Logger { get; }
        protected IStringLocalizer StringLocalizer { get; }
        protected readonly DbSet<lo.Localidade> _dbSet;
        private const int REGISTRO_POR_PAGINA_PADRAO = 100;

        public RepositorioLocalidade(IContextoAplicacao contexto, 
            IConfiguration configuration, 
            ILogger logger, 
            IStringLocalizer stringLocalizer)
        {
            Contexto = contexto;
            Configuration = configuration;
            Logger = logger;
            StringLocalizer = stringLocalizer;
            _dbSet = Contexto.Instance.Set<lo.Localidade>();
        }

        public void Remove(lo.Localidade entidade)
        {
            Contexto.Localidades.Remove(entidade);
        }

        public void Remove(Guid id)
        {
            lo.Localidade entidade = Carrega(id);
            if (entidade == null)
                throw new NaoEncontradoException(StringLocalizer["LocalidadeNaoEncontrado"]);
            Remove(entidade);
        }

        public void Insere(lo.Localidade entidade)
        {
            Contexto.Localidades.Add(entidade);
        }

        public void Atualiza(lo.Localidade entidade)
        {
            Contexto.Instance.Entry(entidade).State = EntityState.Modified;
        }

        public lo.Localidade Carrega(Guid id)
        {
            return Contexto.Localidades.Find(id);
        }

        public IEnumerable<lo.Localidade> Pesquisa(OpcoesPesquisa opcoes)
        {
            var pr = PredicateBuilder.New<lo.Localidade>(true);

            int? registros = opcoes?.RegistrosPorPagina ?? REGISTRO_POR_PAGINA_PADRAO;
            return Contexto.Localidades.Where(pr).Paginador(opcoes?.Pagina, registros);
        }
        public IEnumerable<lo.Localidade> PesquisaTodos(OpcoesPesquisa opcoes)
        {

            int? registros = opcoes?.RegistrosPorPagina ?? REGISTRO_POR_PAGINA_PADRAO;

            var query = (from localidade in Contexto.Localidades.Include(p=>p.Sensores)
                         join regiao in Contexto.Regioes on localidade.IdRegiao equals regiao.Id
                         join pais in Contexto.Paises on localidade.IdPais equals pais.Id
                         select new { localidade, regiao, pais }).Paginador(opcoes?.Pagina, registros).ToList();
            return query.Select(p=>p.localidade);
        }
    }
}