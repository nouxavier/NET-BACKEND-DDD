
using ChallengeDominio.VO.EventoSensor.V1;
using ChallengeDominio.VO.Localidades.V1;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ChallengeMVC.Models
{
    public class EventoSensorViewModel
    {
        public IEnumerable<EventoSensorVO> EventosSensoresVO { get; set; }
        public IEnumerable<LocalidadeVO> LocalidadesVO { get; set; }
        public SelectList RegiaoSelect { get; set; }
    }
}
