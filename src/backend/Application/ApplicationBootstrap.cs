
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    namespace backend.Application
    {
        /// <summary>
        /// Simple bootstrap class for initialize application context and other instances
        /// </summary>
        internal sealed class ApplicationBootrstrap : BackgroundService
        {
            private readonly ApplicationContext _context;
            private readonly ILogger<ApplicationBootrstrap> _logger;

            public ApplicationBootrstrap(ApplicationContext context, ILogger<ApplicationBootrstrap> logger)
            {
                _context = context;
                _logger = logger;
            }
            protected override Task ExecuteAsync(CancellationToken stoppingToken)
            {
                _logger.LogInformation("Bootstrap start");
                _context.Start();
                _logger.LogInformation("Bootstrap complete");
                return Task.CompletedTask;
            }
        }
    }