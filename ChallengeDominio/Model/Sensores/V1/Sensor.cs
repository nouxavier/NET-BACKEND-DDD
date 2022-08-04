using ChallengeDominio.Model.Localidades.V1;
using System;
using System.Collections.Generic;

namespace ChallengeDominio.Model.Sensores.V1
{
    public class Sensor : IEntidadeConcorrente
    {
        public uint xmin { get; set; }
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public ICollection<EventoSensor> EventosSensores { get; set; }
        public Guid IdLocalidade { get; set; } 
        public Localidade Localidade { get; set; }

    }
}
