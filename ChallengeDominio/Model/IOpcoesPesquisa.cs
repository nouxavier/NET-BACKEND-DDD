namespace ChallengeDominio
{
    public interface IOpcoesPesquisa
    {

        int? RegistrosPorPagina { get; set; }
        int? Pagina { get; set; }

        void Valida();
    }
}