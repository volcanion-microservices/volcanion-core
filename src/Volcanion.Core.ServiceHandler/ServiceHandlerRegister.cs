using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volcanion.Core.ServiceHandler.Abstractions;
using Volcanion.Core.ServiceHandler.Implementations;

namespace Volcanion.Core.ServiceHandler;

/// <summary>
/// ServiceHandlerRegister
/// </summary>
public static class ServiceHandlerRegister
{
    /// <summary>
    /// AddKafkaProducerService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddKafkaProducerService(this IServiceCollection services, IConfiguration configuration)
    {
        var kafkaBootstrapServer = configuration.GetSection("Redis");
        var config = new ProducerConfig { BootstrapServers = kafkaBootstrapServer["url"] };
        var builder = new ProducerBuilder<Null, string>(config);
        builder.SetKeySerializer(Serializers.Null);
        builder.SetValueSerializer(Serializers.Utf8);
        var producer = builder.Build();
        services.AddSingleton(producer);
        return services;
    }

    /// <summary>
    /// AddKafkaConsumerService
    /// </summary>
    /// <param name="services"></param>
    public static void AddKafkaConsumerService(this IServiceCollection services)
    {
        services.AddHostedService<KafkaConsumerService>();
        services.AddSingleton<IKafkaProducerService, KafkaProducerService>();
    }
}
