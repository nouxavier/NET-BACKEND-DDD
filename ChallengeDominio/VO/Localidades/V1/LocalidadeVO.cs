using System;


namespace ChallengeDominio.VO.Localidades.V1
{
    public class LocalidadeVO
    {
        public Guid Id { get; set; }
        public PaisVO Pais { get; set; }
        public RegiaoVO Regiao { get; set; }
    }
}
