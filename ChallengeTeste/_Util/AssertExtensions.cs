using ChallengeDominio.Excecoes;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ChallengeTeste._Util
{
    public static class AssertExtensions
    {
        public static void ThrowsWithMessage(Action testCode, string messageExpected)
        {
            var message = Assert.Throws<DominioException>(testCode).Message;
            Assert.Equal(messageExpected, message);
        }

        public static void ThrowsWithMessageAsync(Func<Task> testCode, string messageExpected)
        {
            var result = Assert.ThrowsAsync<DominioException>(testCode).Result;
            Assert.Equal(messageExpected, result.Message);
        }
    }
}
