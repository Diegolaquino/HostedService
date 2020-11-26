using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HostedService
{
    public class Worker : IWorker
    {
        private readonly ILogger<Worker> _logger;
        private int number = 0;

        public Worker(ILogger<Worker> logger)
        {
            this._logger = logger;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Interlocked.Increment(ref number);
                _logger.LogInformation($"worker printando na tela: { number }");

                await Task.Delay(1000 * 5);
            }
        }
    }

    public interface IWorker
    {
        public Task DoWork(CancellationToken cancellationToken);
    }
}
