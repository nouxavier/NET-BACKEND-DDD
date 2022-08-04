using System;


namespace ChallengeDominio.Excecoes
{
    public class CacheException : Exception
    {
        public CacheException()
        {
        }

        public CacheException(string msg) : base(msg)
        {
        }

        public CacheException(string msg, Exception ex) : base(msg, ex)
        {
        }

    }
}
