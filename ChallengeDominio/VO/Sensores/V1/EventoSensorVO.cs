using System;

namespace ChallengeDominio.VO.EventoSensor.V1
{
    public class EventoSensorVO
    {
        public Guid Id { get; set; }
        public Int64 TimeStamp { get; set; }
        public string Valor { get; set; }
        public string Tag { get; set; }

        public string Status { 
            get{
                if (Valor != null)
                    return "Processado";
                else
                    return "Não Processado";
            } 
        }
    }
}
