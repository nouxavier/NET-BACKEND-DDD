using System;


namespace ChallengeDominio.Excecoes
{
    public class PossuiReferenciaException : Exception
    {
        public PossuiReferenciaException()
        {
        }

        public PossuiReferenciaException(string msg) : base(msg)
        {
        }

        public PossuiReferenciaException(string msg, Exception ex) : base(msg, ex)
        {
        }

    }
}
