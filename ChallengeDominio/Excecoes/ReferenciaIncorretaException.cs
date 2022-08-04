using System;


namespace ChallengeDominio.Excecoes
{
    public class ReferenciaIncorretaException : Exception
    {
        public ReferenciaIncorretaException()
        {
        }

        public ReferenciaIncorretaException(string msg) : base(msg)
        {
        }

        public ReferenciaIncorretaException(string msg, Exception ex) : base(msg, ex)
        {
        }

    }
}
