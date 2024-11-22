namespace Volcanion.Core.ServiceHandler.Abstractions;

/// <summary>
/// IKafkaConsumerService
/// </summary>
public interface IKafkaConsumerService
{
    /// <summary>
    /// StartAsync
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    Task StartAsync(CancellationToken stoppingToken);

    /// <summary>
    /// StopAsync
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    Task StopAsync(CancellationToken stoppingToken);
}
