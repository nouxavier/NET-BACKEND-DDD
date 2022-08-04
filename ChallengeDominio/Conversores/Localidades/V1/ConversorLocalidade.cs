

using ChallengeDominio.Model.Localidades.V1;
using ChallengeDominio.VO.Localidades.V1;
using System.Collections.Generic;

namespace ChallengeDominio.Conversores.Localidades.V1
{
    public class ConversorLocalidade : IConversor<Localidade, LocalidadeVO>
    {
        public LocalidadeVO Converte(Localidade origem)
        {
            var resp = new LocalidadeVO();
            CopiaPropriedades(origem, resp);
            return resp;
        }

        public Localidade Converte(LocalidadeVO origem)
        {
            var resp = new Localidade();
            CopiaPropriedades(origem, resp);
            return resp;
        }
        public IEnumerable<LocalidadeVO> ListaConverte(IEnumerable<Localidade> origem)
        {
            var resp = new List<LocalidadeVO>();
            foreach (var item in origem)
                resp.Add(Converte(item));
            return resp;
        }
        public IEnumerable<Localidade> ListaConverte(IEnumerable<LocalidadeVO> origem)
        {
            var resp = new List<Localidade>();
            foreach (var item in origem)
                resp.Add(Converte(item));
            return resp;
        }
        public void CopiaPropriedades(Localidade origem, LocalidadeVO destino)
        {
            destino.Id = origem.Id;
            destino.Pais = new ConversorPais().Converte(origem.Pais);
            destino.Regiao = new ConversorRegiao().Converte(origem.Regiao);
        }

        public void CopiaPropriedades(LocalidadeVO origem, Localidade destino)
        {
            destino.Id = origem.Id;
            destino.Pais = new ConversorPais().Converte(origem.Pais);
            destino.Regiao = new ConversorRegiao().Converte(origem.Regiao);
        }
    }

}
