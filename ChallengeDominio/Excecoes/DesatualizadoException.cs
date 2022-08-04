

using System;

namespace ChallengeDominio.Excecoes
{
    public class DesatualizadoException : Exception
    {
        public DesatualizadoException()
        {
        }

        public DesatualizadoException(string msg) : base(msg)
        {
        }

        public DesatualizadoException(string msg, Exception ex) : base(msg, ex)
        {
        }

    }
}
