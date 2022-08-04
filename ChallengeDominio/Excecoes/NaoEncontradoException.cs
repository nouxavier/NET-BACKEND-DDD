using System;

namespace ChallengeDominio.Excecoes
{
    public class NaoEncontradoException : Exception
    {
        public NaoEncontradoException()
        {
        }

        public NaoEncontradoException(string msg) : base(msg)
        {
        }

        public NaoEncontradoException(string msg, Exception ex) : base(msg, ex)
        {
        }

    }
}
