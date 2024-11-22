using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using Volcanion.Core.ServiceHandler.Abstractions;

namespace Volcanion.Core.ServiceHandler.Implementations;

/// <inheritdoc />
internal class KafkaConsumerService : BackgroundService, IKafkaConsumerService
{
    /// <summary>
    /// ILogger instance
    /// </summary>
    private readonly ILogger<KafkaConsumerService> _logger;

    /// <summary>
    /// ConsumerConfig instance
    /// </summary>
    private readonly ConsumerConfig _config;

    /// <summary>
    /// IConsumer instance
    /// </summary>
    private readonly IConsumer<Ignore, string> _consumer;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    public KafkaConsumerService(ILogger<KafkaConsumerService> logger)
    {
        _config = new ConsumerConfig
        {
            GroupId = "my-consumer-group",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest

        };

        _consumer = new ConsumerBuilder<Ignore, string>(_config).Build();

        _consumer.Subscribe("employee-events");

        _logger = logger;
    }

    /// <inheritdoc />
    public Task StartAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe("employee-events");
        return base.StartAsync(stoppingToken);
    }

    /// <inheritdoc />
    public Task StopAsync(CancellationToken stoppingToken)
    {
        _consumer.Close();
        return base.StopAsync(stoppingToken);
    }

    /// <summary>
    /// ExecuteAsync
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var consumeResult = _consumer.Consume(stoppingToken);
            Console.WriteLine("Received {0} {1}", consumeResult.Key, consumeResult.Message);
            await Task.CompletedTask;
        }
    }
}
