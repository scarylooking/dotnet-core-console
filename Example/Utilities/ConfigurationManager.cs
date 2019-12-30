using System;
using example.Models.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace example.Utilities
{
    public interface IConfigurationManager
    {
        App App { get; }
    }

    public class ConfigurationManager : IConfigurationManager
    {
        private readonly ILogger<ConfigurationManager> _logger;
        private readonly IConfiguration _configuration;
        
        public App App { get; private set; }

        public ConfigurationManager(IConfiguration configuration, ILogger<ConfigurationManager> logger)
        {
            _logger = logger;
            _configuration = configuration;

            LoadAppConfiguration();
        }

        private void LoadAppConfiguration()
        {
            try
            {
                App = LoadConfig<App>("environment");
            }
            catch (Exception ex)
            {
                _logger.LogError("failed to load environment config", ex);
            }
        }

        private T LoadConfig<T>(string configName)
        {
            _logger.LogDebug($"attempting to load config section '{configName}'");

            var configObject = _configuration.GetSection(configName).Get<T>();

            _logger.LogTrace("'{configName}' config loaded: {@configObject}", configName, configObject);

            return configObject;
        }
    }
}