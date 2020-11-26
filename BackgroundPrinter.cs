using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HostedService
{
    public class BackgroundPrinter : IHostedService
    {
        private readonly ILogger<BackgroundPrinter> logger;
        private int _number = 0;
        private Timer _timer;
        private IWorker _worker;

        public BackgroundPrinter(ILogger<BackgroundPrinter> logger, IWorker worker)
        {
            this.logger = logger;
            this._worker = worker;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _worker.DoWork(cancellationToken);
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
