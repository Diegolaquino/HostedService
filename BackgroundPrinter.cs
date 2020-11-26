using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HostedService
{
    public class BackgroundPrinter : IHostedService, IDisposable
    {
        private readonly ILogger<BackgroundPrinter> logger;
        private int _number = 0;
        private Timer _timer;

        public BackgroundPrinter(ILogger<BackgroundPrinter> logger)
        {
            this.logger = logger;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(o =>
            {
                Interlocked.Increment(ref _number);
                logger.LogInformation($"Eu aqui de novo seus putos -> {_number}");
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Vou parar por aqui seus putos");

            _timer = new Timer(o =>
            {
                Interlocked.Increment(ref _number);
            }, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }
    }
}
