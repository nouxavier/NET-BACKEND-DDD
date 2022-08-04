

using ChallengeAplicacao.Repositorio;
using ChallengeAplicacao.Repositorio.Localidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;

namespace ChallengeRepositorioPostgresql.Repositorio.Localidade
{
    public class UoWAplicacao : IUoWAplicacao, IDisposable
    {
        private bool disposedValue;
        private IRepositorioRegiao _repositorioRegiao;
        private IRepositorioPais _repositorioPais;
        private IRepositorioLocalidade _repositorioLocalidade;
        private IRepositorioSensor _repositorioSensor;
        private IRepositorioEventoSensor _repositorioEventoSensor;

        private IContextoAplicacao Contexto { get; }
        private IConfiguration Configuration { get; }
        private ILogger Logger { get; }
        private IStringLocalizer StringLocalizer { get; }

        public IRepositorioLocalidade RepositorioLocalidade
        {
            get => _repositorioLocalidade ??= new RepositorioLocalidade(Contexto, Configuration, Logger, StringLocalizer);
        }

        public IRepositorioPais RepositorioPais
        {
            get => _repositorioPais ??= new RepositorioPais(Contexto, Configuration, Logger, StringLocalizer);
        }

        public IRepositorioRegiao RepositorioRegiao
        {
            get => _repositorioRegiao ??= new RepositorioRegiao(Contexto, Configuration, Logger, StringLocalizer);
        }
        public IRepositorioSensor RepositorioSensor
        {
            get => _repositorioSensor ??= new RepositorioSensor(Contexto, Configuration, Logger, StringLocalizer);
        }
        public IRepositorioEventoSensor RepositorioEventoSensor
        {
            get => _repositorioEventoSensor ??= new RepositorioEventoSensor(Contexto, Configuration, Logger, StringLocalizer);
        }

        public UoWAplicacao(IConfiguration configuration,
           ILogger logger,
           IStringLocalizer stringLocalizer)
        {
            Configuration = configuration;
            Logger = logger;
            StringLocalizer = stringLocalizer;

            var cs = configuration.GetConnectionString("Challenge");

            if (string.IsNullOrWhiteSpace(cs))
            {
                logger.LogError("ConnectionString do Challenge não configurado.");
                throw new ApplicationException(stringLocalizer["ErroDeConfiguracao"]);
            }

            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseNpgsql(cs)
                .UseSnakeCaseNamingConvention();

            Contexto = new ContextoAplicacao(builder.Options);

        }

        public void Commit()
        {
            Contexto.Instance.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

       

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
