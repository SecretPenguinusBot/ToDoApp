using System;
using System.Threading;
using System.Threading.Tasks;
using backend.Application;
using backend.Application.ServerTime;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace backend
{
    internal class Program
    {
        private ILogger<Program> _logger;
        private IHost _host;
        public Program(string[] args)
        {
            _host = CreateAppHostBuilder(args).Build();
            _logger = _host.Services.GetRequiredService<ILogger<Program>>();
        }

        public async Task RunAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Application starting");
            try
            {
                await _host.RunAsync(cancellationToken);

            }
            catch(OperationCanceledException)
            { 
                _logger.LogInformation("Aplication stopped cause stop request"); 
            }
            catch(Exception ex)
            {
                 _logger.LogError($"Critical error: {ex}"); 
            }
            _logger.LogInformation("Application complete execution");
        }

        private static IHostBuilder CreateAppHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder()
                .ConfigureLogging(ConfigureApplicationLog)
                .ConfigureServices(ConfigureApplicationService)
                .ConfigureWebHostDefaults(webHostBuilder => webHostBuilder.UseStartup<Startup>());
        
        private static void ConfigureApplicationService(HostBuilderContext ctx, IServiceCollection services)
        {
            services.AddSingleton<IServerTime, ServerTimeImpl>();
            services.AddSingleton<ApplicationContext>();
            services.AddHostedService<ApplicationBootrstrap>();
        }

        private static void ConfigureApplicationLog(HostBuilderContext ctx, ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddConsole();
            loggingBuilder.AddSerilog();
        }
    }
}
