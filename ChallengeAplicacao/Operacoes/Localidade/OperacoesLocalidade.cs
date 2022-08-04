
using ChallengeAplicacao.Repositorio;
using ChallengeDominio._Util;
using ChallengeDominio.Cache.Localidade;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using lo = ChallengeDominio.Model.Localidades.V1;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ChallengeAplicacao.Operacoes.Localidade
{
    public class OperacoesLocalidade : IOperacoesLocalidade
    {
        private ILogger Logger { get; }
        private IServiceProvider Provider { get; }
        private IStringLocalizer StringLocalizer { get; }
        private ITradutorExcecao TradutorExcecao { get; }
        private ICacheLocalidade CacheLocalidade { get; }

        public OperacoesLocalidade(IServiceProvider provider,
            ILogger logger, 
            IStringLocalizer stringLocalizer,
            ITradutorExcecao tradutorExcecao,
            ICacheLocalidade cacheLocalidade
           )
        {
            Provider = provider;
            Logger = logger;
            StringLocalizer = stringLocalizer;
            TradutorExcecao = tradutorExcecao;
            CacheLocalidade = cacheLocalidade;
        }
        public IEnumerable<lo.Localidade> CarregaCache()
        {
            List<lo.Localidade> localidades = new List<lo.Localidade>();
            if (!CacheLocalidade.TryGetValue(localidades))
            {
                var work = CriaUoWAplicacao();
                localidades = work.RepositorioLocalidade.PesquisaTodos(null).ToList();
                CacheLocalidade.Start(localidades);
                return localidades;
            }
            return CacheLocalidade.Get();
        }

        #region Utils
        private IUoWAplicacao CriaUoWAplicacao() =>
            (IUoWAplicacao)Provider.GetService<IUoWAplicacao>();
        #endregion
    }
}
