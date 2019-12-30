using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using example.Utilities;
using Microsoft.Extensions.Logging;

namespace example
{
    public class Application
    {
        private readonly ILogger<Application> _logger;
        private readonly IConfigurationManager _config;

        public Application(
            ILogger<Application> logger, IConfigurationManager config)
        {
            _logger = logger;
            _config = config;
        }

        public void Run()
        {
            // Do stuff...
        }
    }
}