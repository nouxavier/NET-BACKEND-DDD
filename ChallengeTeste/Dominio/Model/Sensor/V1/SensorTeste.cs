
using ChallengeDominio.Model.Sensores.V1;
using System;
using Xunit;
using Microsoft.Extensions.Localization;
using NSubstitute;
using ExpectedObjects;
using ChallengeDominio.VO.EventoSensor.V1;

namespace ChallengeTeste.Dominio.Model.V1
{
    public class SensorTeste
    {
        private readonly IStringLocalizer _stringLocalizer;
        private readonly Guid Id = Guid.NewGuid();
        private readonly byte[] TimeStamp = Geradores.GeraTimeStamp();
        private readonly string Tag = Geradores.GeraTag();
        private readonly string Valor = Geradores.GeraValor();
        public SensorTeste()
        {
            _stringLocalizer = Substitute.For<IStringLocalizer>();
        }

        [Fact]
        public void Deve_criar_sensor()
        {
            var sensorEsperado = new
            {
                Id = Guid.NewGuid(),
                TimeStamp = Geradores.GeraTimeStamp(),
                Tag = Geradores.GeraTag(),
                Valor = Geradores.GeraValor()
            };

            var sensor = new EventoSensorVO();

            sensorEsperado.ToExpectedObject().ShouldMatch(sensor);

        }

    }
}
