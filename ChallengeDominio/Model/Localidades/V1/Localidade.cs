using System;
using System.Collections.Generic;
using ChallengeDominio.Model.Sensores.V1;

namespace ChallengeDominio.Model.Localidades.V1
{
    public class Localidade : IEntidadeConcorrente
    { 
       
        public uint xmin { get; set; }
        public Guid Id { get; set; }

        public Guid IdPais { get; set; }
        public Pais Pais { get; set; }
        public Guid IdRegiao { get; set; }
        public Regiao Regiao { get; set; }
        public ICollection<EventoSensor> EventosSensores { get; set; }
        public ICollection<Sensor> Sensores { get; set; }
    }
}
