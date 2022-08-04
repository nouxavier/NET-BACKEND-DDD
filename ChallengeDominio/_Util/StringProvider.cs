using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ChallengeDominio._Util
{
    public class StringProvider : IStringLocalizer
    {
        private readonly ILogger _logger;

        private readonly Dictionary<string, string> valores = new Dictionary<string, string>();

        public StringProvider(IConfiguration configuration, ILogger<StringProvider> logger)
        {
            this._logger = logger;

            string arquivo = configuration.GetSection("Language:Arquivo").Value;
            if (arquivo == null)
                logger.LogError("Configuração não possui a seção Language:Arquivo configurada.");
            else
                valores = CarregaValores(arquivo);
        }

        #region IStringLocalizer

        public LocalizedString this[string name]
        {
            get
            {
                if (valores.ContainsKey(name))
                    return new LocalizedString(name, valores[name]);
                else
                    return new LocalizedString(name, $"[{name}]", true);
            }
        }

        public LocalizedString this[string name, params object[] arguments] => this[name];

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return from string key in valores.Keys
                   select new LocalizedString(key, valores[key]);
        }

        #endregion

        private Dictionary<string, string> CarregaValores(string pathArquivo)
        {

            if (!File.Exists(pathArquivo))
                _logger.LogError($"Arquivo de language '{pathArquivo}' não encontrado.");
            try
            {
                return JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(pathArquivo));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Arquivo '{pathArquivo}' possui problemas de formatação.");
                return new Dictionary<string, string>();
            }
        }


    }
}
