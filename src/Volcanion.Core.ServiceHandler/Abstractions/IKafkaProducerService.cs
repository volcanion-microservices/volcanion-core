namespace Volcanion.Core.ServiceHandler.Abstractions;

/// <summary>
/// IKafkaProducerService
/// </summary>
public interface IKafkaProducerService
{
    /// <summary>
    /// SendMessageAsync
    /// </summary>
    /// <param name="topic"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task SendMessageAsync(string topic, object message);
}
