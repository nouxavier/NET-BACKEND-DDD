using ChallengeDominio._Util;
using Microsoft.Extensions.Localization;

namespace ChallengeDominio
{
    public class OpcoesPesquisa: IOpcoesPesquisa
    {

        protected IStringLocalizer StringLocalizer { get; }


        public int? RegistrosPorPagina { get; set; }
        public int? Pagina { get; set; }

        public OpcoesPesquisa(IStringLocalizer stringLocalizer)
        {
            StringLocalizer = stringLocalizer;
        }
        /// <summary>
        /// Valida se as opções de pesquisa estão ok.
        /// </summary>
        /// <returns>Vazio se ok</returns>
        public virtual void Valida()
        {

            var validador = new Validador(StringLocalizer);
            validador.Intervalo(RegistrosPorPagina, PropertyHelper.ClassAndPropertyName(() => this.RegistrosPorPagina), 1, null);
            validador.Intervalo(Pagina, PropertyHelper.ClassAndPropertyName(() => this.Pagina), 1, null);

        }
    }
}