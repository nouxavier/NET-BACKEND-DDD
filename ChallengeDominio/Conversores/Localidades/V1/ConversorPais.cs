using ChallengeDominio.Model.Localidades.V1;
using ChallengeDominio.VO.Localidades.V1;
using System.Collections.Generic;

namespace ChallengeDominio.Conversores.Localidades.V1
{
    public class ConversorPais : IConversor<Pais, PaisVO>
    {
        public PaisVO Converte(Pais origem)
        {
            var resp = new PaisVO();
            CopiaPropriedades(origem, resp);
            return resp;
        }

        public Pais Converte(PaisVO origem)
        {
            var resp = new Pais();
            CopiaPropriedades(origem, resp);
            return resp;
        }
        public IEnumerable<PaisVO> ListaConverte(IEnumerable<Pais> origem)
        {
            var resp = new List<PaisVO>();
            foreach (var item in origem)
                resp.Add(Converte(item));
            return resp;
        }
        public IEnumerable<Pais> ListaConverte(IEnumerable<PaisVO> origem)
        {
            var resp = new List<Pais>();
            foreach (var item in origem)
                resp.Add(Converte(item));
            return resp;
        }
        public void CopiaPropriedades(Pais origem, PaisVO destino)
        {
            destino.Id = origem.Id;
            destino.Nome = origem.Nome;
        }

        public void CopiaPropriedades(PaisVO origem, Pais destino)
        {
            destino.Id = origem.Id;
            destino.Nome = origem.Nome;
        }
    }
}
