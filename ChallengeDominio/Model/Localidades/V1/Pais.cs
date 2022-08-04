using System;
using System.Collections.Generic;


namespace ChallengeDominio.Model.Localidades.V1
{
    public class Pais: IEntidadeConcorrente
    {
        public uint xmin { get; set; }
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public  ICollection<Localidade> Localidades { get; set; }
       
    }
}
