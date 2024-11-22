using Confluent.Kafka;
using System.Text.Json;
using Volcanion.Core.ServiceHandler.Abstractions;

namespace Volcanion.Core.ServiceHandler.Implementations;

/// <inheritdoc />
internal class KafkaProducerService : IKafkaProducerService
{
    /// <summary>
    /// IProducer instance
    /// </summary>
    private readonly IProducer<Null, string> _producer;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="brokerList"></param>
    public KafkaProducerService(string brokerList)
    {
        var config = new ProducerConfig { BootstrapServers = brokerList };
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    /// <inheritdoc />
    public async Task SendMessageAsync(string topic, object message)
    {
        var jsonMessage = JsonSerializer.Serialize(message);
        await _producer.ProduceAsync(topic, new Message<Null, string> { Value = jsonMessage });
    }
}
