using ChallengeDominio.Model.Localidades.V1;
using ChallengeDominio.VO.Localidades.V1;
using System.Collections.Generic;


namespace ChallengeDominio.Conversores.Localidades.V1
{
    class ConversorRegiao : IConversor<Regiao, RegiaoVO>
    {
        public RegiaoVO Converte(Regiao origem)
        {
            var resp = new RegiaoVO();
            CopiaPropriedades(origem, resp);
            return resp;
        }

        public Regiao Converte(RegiaoVO origem)
        {
            var resp = new Regiao();
            CopiaPropriedades(origem, resp);
            return resp;
        }
        public IEnumerable<RegiaoVO> ListaConverte(IEnumerable<Regiao> origem)
        {
            var resp = new List<RegiaoVO>();
            foreach (var item in origem)
                resp.Add(Converte(item));
            return resp;
        }
        public IEnumerable<Regiao> ListaConverte(IEnumerable<RegiaoVO> origem)
        {
            var resp = new List<Regiao>();
            foreach (var item in origem)
                resp.Add(Converte(item));
            return resp;
        }
        public void CopiaPropriedades(Regiao origem, RegiaoVO destino)
        {
            destino.Id = origem.Id;
            destino.Nome = origem.Nome;
        }

        public void CopiaPropriedades(RegiaoVO origem, Regiao destino)
        {
            destino.Id = origem.Id;
            destino.Nome = origem.Nome;
        }

    }
}
