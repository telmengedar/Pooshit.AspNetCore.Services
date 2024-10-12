using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Pooshit.AspNetCore.Services;

/// <summary>
/// executes background tasks intervals
/// </summary>
public abstract class BackgroundIntervalWorker : BackgroundService {
    readonly ILogger logger;

    /// <summary>
    /// creates a new <see cref="BackgroundIntervalWorker"/>
    /// </summary>
    /// <param name="logger">access to logging</param>
    /// <param name="taskinterval">interval used to execute tasks</param>
    /// <param name="pollinterval">interval used to check whether task should be executed</param>
    protected BackgroundIntervalWorker(ILogger logger, TimeSpan taskinterval, TimeSpan pollinterval) {
        this.logger = logger;
        TaskInterval = taskinterval;
        PollInterval = pollinterval;
    }

    /// <summary>
    /// interval used to execute process
    /// </summary>
    protected TimeSpan TaskInterval { get; }

    /// <summary>
    /// interval used to execute process
    /// </summary>
    protected TimeSpan PollInterval { get; }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        DateTime lastcheck = DateTime.MinValue;
        while (!stoppingToken.IsCancellationRequested) {
            DateTime now = DateTime.Now;
            if (now - lastcheck >= TaskInterval) {
                try {
                    await ExecuteTask(stoppingToken);
                    lastcheck = now;
                }
                catch (Exception e) {
                    logger.LogError(e, "Unable to run background task");
                }
            }
            await Task.Delay(PollInterval, stoppingToken);
        }
    }

    /// <summary>
    /// task to execute regularly
    /// </summary>
    protected new abstract Task ExecuteTask(CancellationToken cancellationtoken);
}