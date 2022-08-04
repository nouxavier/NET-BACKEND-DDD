using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeDominio.Cache
{
    public interface ICache<TEntity>
    {
        void Start(TEntity entity);
        bool TryGetValue(TEntity entity);
        TEntity Get();
        void Restart(TEntity entity);
        void Remove();
    }
}
