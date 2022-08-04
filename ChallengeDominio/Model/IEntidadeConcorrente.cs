
namespace ChallengeDominio
{
    public interface IEntidadeConcorrente : IEntidade
    {
#pragma warning disable IDE1006 // Naming Styles
        uint xmin { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
