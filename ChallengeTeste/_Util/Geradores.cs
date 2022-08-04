using System;

namespace ChallengeTeste
{
    public class Geradores
    {
        public static byte[] GeraTimeStamp()
        {
            DateTime utcNow = DateTime.UtcNow;
            long utcNowAsLong = utcNow.ToBinary();
            return  BitConverter.GetBytes(utcNowAsLong);
        }

        public static string GeraTag()
        {
            return "brasil.sudeste.sensor01";
        }

        public static string GeraValor()
        {
            return "2 mA";
        }
    }
}
