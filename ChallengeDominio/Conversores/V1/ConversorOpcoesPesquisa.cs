using ChallengeDominio.VO;
using System;


namespace ChallengeDominio.Conversores.V1
{
    public class ConversorOpcoesPesquisa : IConversor<IOpcoesPesquisa, OpcoesPesquisaVO>
    {

        private readonly IServiceProvider serviceProvider;

        public ConversorOpcoesPesquisa(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public OpcoesPesquisaVO Converte(IOpcoesPesquisa origem)
        {
            var resp = new OpcoesPesquisaVO();
            CopiaPropriedades(origem, resp);
            return resp;
        }

        public IOpcoesPesquisa Converte(OpcoesPesquisaVO origem)
        {
            IOpcoesPesquisa resp = (IOpcoesPesquisa)serviceProvider.GetService(typeof(IOpcoesPesquisa));
            CopiaPropriedades(origem, resp);
            return resp;
        }

        public void CopiaPropriedades(IOpcoesPesquisa origem, OpcoesPesquisaVO destino)
        {
            if (origem == null)
                throw new ArgumentNullException(nameof(origem));
            if (destino == null)
                throw new ArgumentNullException(nameof(destino));
            destino.Pagina = origem.Pagina;
            destino.RegistrosPorPagina = origem.RegistrosPorPagina;
        }

        public void CopiaPropriedades(OpcoesPesquisaVO origem, IOpcoesPesquisa destino)
        {
            if (origem == null)
                throw new ArgumentNullException(nameof(origem));
            if (destino == null)
                throw new ArgumentNullException(nameof(destino));
            destino.Pagina = origem.Pagina;
            destino.RegistrosPorPagina = origem.RegistrosPorPagina;
        }
    }
}
