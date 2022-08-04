using ChallengeDominio.Conversores.V1;
using ChallengeDominio.Model.Sensores;
using ChallengeDominio.VO.Sensores.V1;
using System;

namespace ChallengeDominio.Conversores.Sensores.V1
{
    public class ConversorOpcoesEventosSensores : ConversorOpcoesPesquisa, IConversor<IOpcoesEventosSensores, OpcoesEventosSensoresVO>
    {
        private readonly IServiceProvider serviceProvider;

        public ConversorOpcoesEventosSensores(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public OpcoesEventosSensoresVO Converte(IOpcoesEventosSensores origem)
        {
            var resp = new OpcoesEventosSensoresVO();
            CopiaPropriedades(origem, resp);
            return resp;

        }

        public IOpcoesEventosSensores Converte(OpcoesEventosSensoresVO origem)
        {
            IOpcoesEventosSensores resp = (IOpcoesEventosSensores)serviceProvider.GetService(typeof(IOpcoesEventosSensores));
            CopiaPropriedades(origem, resp);
            return resp;
        }

        public void CopiaPropriedades(IOpcoesEventosSensores origem, OpcoesEventosSensoresVO destino)
        {
            base.CopiaPropriedades(origem, destino);
            destino.Pais = origem.Pais;
            destino.Sensor = origem.Sensor;
            destino.Regiao = origem.Regiao;
        }

        public void CopiaPropriedades(OpcoesEventosSensoresVO origem, IOpcoesEventosSensores destino)
        {
            base.CopiaPropriedades(origem, destino);
            destino.Pais = origem.Pais;
            destino.Sensor = origem.Sensor;
            destino.Regiao = origem.Regiao;
        }
    }
}
