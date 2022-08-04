using ChallengeDominio.Cache.Localidade;
using ChallengeDominio.Excecoes;
using ChallengeDominio.Model.Localidades.V1;
using ChallengeDominio.Model.Sensores.V1;
using ChallengeDominio.VO.EventoSensor.V1;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChallengeDominio.Conversores.Sensores.V1
{
    public class ConversorEventoSensor : IConversor<EventoSensor, EventoSensorVO>
    {
        private IStringLocalizer StringLocalizer { get; }
        private ICacheLocalidade CacheLocalidade { get; }

        public ConversorEventoSensor(ICacheLocalidade cacheLocalidade, IStringLocalizer stringLocalizer)
        {
            CacheLocalidade = cacheLocalidade;
            StringLocalizer = stringLocalizer;
        }
        public EventoSensorVO Converte(EventoSensor origem)
        {
            var resp = new EventoSensorVO();
            CopiaPropriedades(origem, resp);
            return resp;
        }
        public IEnumerable<EventoSensorVO> ListaConverte(IEnumerable<EventoSensor> origem)
        {
            var resp = new List<EventoSensorVO>();
            foreach(var item in origem)
                resp.Add(Converte(item));
            return resp;
        }

        public IEnumerable<EventoSensor> ListaConverte(IEnumerable<EventoSensorVO> origem)
        {
            var resp = new List<EventoSensor>();
            foreach (var item in origem)
                resp.Add(Converte(item));
            return resp;
        }

        public EventoSensor Converte(EventoSensorVO origem)
        {
            var resp = new EventoSensor();
            CopiaPropriedades(origem, resp);
            return resp;
        }

        public void CopiaPropriedades(EventoSensor origem, EventoSensorVO destino)
        {
            List<Localidade> localidades = new List<Localidade>();
            if (!CacheLocalidade.TryGetValue(localidades))
                throw new CacheException(StringLocalizer["Cache.Localidade"]);

            destino.Id = origem.Id;
            destino.TimeStamp =  origem.TimeStamp;
            destino.Valor = origem.Valor;

            //Construindo TAG
            //Recuperando pais e regiao
            localidades = CacheLocalidade.Get().ToList();
            var localidade = localidades.Where(p => p.Id ==origem.IdLocalidade
                && p.Sensores.ToList().Exists(p => p.Id == origem.IdSensor)).FirstOrDefault();
            destino.Tag = (localidade.Pais.Nome + "." + localidade.Regiao.Nome + "." + localidade.Sensores.Where(p => p.Id == origem.IdSensor).First().Nome).ToLower();
        }

        public void CopiaPropriedades(EventoSensorVO origem, EventoSensor destino)
        {
            //Construindo TAG
            var tagSplit = origem.Tag.ToLower().Split('.');

            List<Localidade> localidades = new List<Localidade>();
            if (!CacheLocalidade.TryGetValue(localidades))
                throw new CacheException(StringLocalizer["Cache.Localidade"]);
           
            localidades = CacheLocalidade.Get().ToList();
            var localidade = localidades.Where(p => p.Pais.Nome.ToLower() == tagSplit[0]
                && p.Regiao.Nome.ToLower() == tagSplit[1].Replace('-', ' ')
                && p.Sensores.ToList().Exists(p => p.Nome == tagSplit[2])).FirstOrDefault();

            destino.Id = origem.Id;
            destino.IdLocalidade = localidade.Id;
            destino.IdSensor = localidade.Sensores.Where(p => p.Nome == tagSplit[2]).FirstOrDefault().Id;
            destino.Valor = origem.Valor;
            destino.TimeStamp = origem.TimeStamp;


        }
    }
}
