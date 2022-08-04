
using ChallengeDominio.VO.EventoSensor.V1;
using ChallengeDominio.VO.Localidades.V1;
using ChallengeSimuladorSensores._Util;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChallengeSimuladorSensores.Services.V1
{
    public class ServiceEventoSensor
    {
        private HttpClient HttpClientFactory { get; }
        private JsonSerializerOptions _options = new JsonSerializerOptions { AllowTrailingCommas = true, PropertyNameCaseInsensitive = true };
        private const string URI = "https://localhost:5001";
        private RestUtils RestUtils { get; }
        public ServiceEventoSensor( HttpClient httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
            RestUtils = new RestUtils(httpClientFactory);
        }

        public EventoSensorVO GetEventoSensorId(string id)
        {
            var jsonString = RestUtils.GetString($"{URI}/api/v1/EventoSensor/" + id).Result;
            EventoSensorVO eventos = JsonSerializer.Deserialize<EventoSensorVO>(jsonString, _options);
            return eventos;
        }

        public void InsereEventos(EventoSensorVO eventoSensorVO)
        {
            string json = JsonSerializer.Serialize(eventoSensorVO, typeof(EventoSensorVO));
            var jsonString = RestUtils.Post($"{URI}/api/v1/EventoSensor", json).Result;
            var eventos = JsonSerializer.Deserialize<EventoSensorVO>(jsonString);
        }

    }
}
