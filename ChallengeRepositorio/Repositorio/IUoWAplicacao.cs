

using ChallengeRepositorio.Repositorio.Localidade;

namespace ChallengeRepositorio.Repositorio
{
    public interface IUoWAplicacao
    {
        public IRepositorioLocalidade RepositorioLocalidade { get; }
        public IRepositorioPais RepositorioPais { get; }
        public IRepositorioRegiao RepositorioRegiao { get; }
        public IRepositorioSensor RepositorioSensor { get; }
        public IRepositorioEventoSensor RepositorioEventoSensor { get; }
    }
}
