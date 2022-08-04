using ChallengeDominio.Cache.Localidade;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using lo = ChallengeDominio.Model.Localidades.V1;

namespace ChallengeAplicacao.Cache.Localidade
{
    public class CacheLocalidade : ICacheLocalidade
    {
        private IMemoryCache _memoryCache;
        private const string KEY = nameof(CacheLocalidade);
       
        public CacheLocalidade(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public IEnumerable<lo.Localidade> Get()
        {
            return (IEnumerable<lo.Localidade>)_memoryCache.Get(KEY);
        }

        public void Remove()
        {
            _memoryCache.Remove(KEY);
        }

        public void Restart(IEnumerable<lo.Localidade> entity)
        {
            Remove();
            Set(entity);
        }

        public void Start(IEnumerable<lo.Localidade> entity)
        {
            if (!TryGetValue(entity))
            {
                Set(entity);
            }
        }
        private void Set(IEnumerable<lo.Localidade> entity)
        {
            _memoryCache.Set(KEY, entity);
        }

        public bool TryGetValue(IEnumerable<lo.Localidade> entity)
        {
            return _memoryCache.TryGetValue(KEY, out _);
        }
    }
}
