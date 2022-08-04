using ChallengeDominio;
using System;
using System.Collections.Generic;


namespace ChallengeAplicacao.Repositorio
{
    public interface  IRepositorioBase<TEntidade, TOpcoes>
        where TEntidade : class, IEntidade
        where TOpcoes : class, IOpcoesPesquisa
    {
        void Remove(TEntidade entidade);
        void Remove(Guid id);
        void Insere(TEntidade entidade);
        void Atualiza(TEntidade entidade);

        TEntidade Carrega(Guid id);
        IEnumerable<TEntidade> Pesquisa(TOpcoes opcoes);
    }
}
