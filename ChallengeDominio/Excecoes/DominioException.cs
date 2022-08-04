using System;
using System.Collections.Generic;


namespace ChallengeDominio.Excecoes
{
    public class DominioException : Exception
    {
        public DominioException()
        {
        }

        public DominioException(string msg) : base(msg)
        {
        }

        public DominioException(string msg, Exception ex) : base(msg, ex)
        {
        }

    }
}
