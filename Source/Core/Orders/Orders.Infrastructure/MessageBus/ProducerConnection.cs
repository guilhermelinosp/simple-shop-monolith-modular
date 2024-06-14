using RabbitMQ.Client;

namespace Order.Infrastructure.MessageBus;

public class ProducerConnection(IConnection connection)
{
    public IConnection Connection { get; private set; } = connection;
}