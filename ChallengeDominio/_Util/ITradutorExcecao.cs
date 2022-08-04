using System;

namespace ChallengeDominio._Util
{
    public interface ITradutorExcecao
    {
        Exception Converte(Exception ex);
    }
}
