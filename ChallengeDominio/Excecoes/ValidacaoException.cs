using System;

namespace ChallengeDominio.Excecoes
{
    public class ValidacaoException : Exception
    {
        public ValidacaoException()
        {
        }

        public ValidacaoException(string msg) : base(msg)
        {
        }

        public ValidacaoException(string msg, Exception ex) : base(msg, ex)
        {
        }

    }
}
