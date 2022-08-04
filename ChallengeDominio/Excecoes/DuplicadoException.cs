using System;


namespace ChallengeDominio.Excecoes
{
    public class DuplicadoException : Exception
    {
        public DuplicadoException()
        {
        }

        public DuplicadoException(string msg) : base(msg)
        {
        }

        public DuplicadoException(string msg, Exception ex) : base(msg, ex)
        {
        }

    }
}
