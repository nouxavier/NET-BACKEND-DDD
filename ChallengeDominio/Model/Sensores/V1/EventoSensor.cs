using ChallengeDominio._Util;
using ChallengeDominio.Model.Localidades.V1;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChallengeDominio.Model.Sensores.V1
{
    public class EventoSensor : IEntidadeConcorrente
    {
        public uint xmin { get; set; }
        public Guid Id { get; set; }
        public Int64 TimeStamp {get;set;}
        public string Valor { get; set; }
        public Guid IdLocalidade { get; set; }
        public Localidade Localidade { get; set; }

        public Guid IdSensor { get; set; }
        public Sensor Sensor { get; set; }
        

        public void Valida(IStringLocalizer stringLocalizer)
        {
            var validador = new Validador(stringLocalizer);
            validador.Obrigatorio(Id, PropertyHelper.ClassAndPropertyName(() => this.Id));
            validador.Obrigatorio(IdLocalidade, PropertyHelper.ClassAndPropertyName(() => this.IdLocalidade));
            validador.Obrigatorio(IdSensor, PropertyHelper.ClassAndPropertyName(() => this.IdSensor));
            validador.Obrigatorio(TimeStamp, PropertyHelper.ClassAndPropertyName(() => this.TimeStamp));
            validador.ValidaRegra(TimeStamp == 0, PropertyHelper.ClassAndPropertyName(() => this.TimeStamp));

        }


    }
}
