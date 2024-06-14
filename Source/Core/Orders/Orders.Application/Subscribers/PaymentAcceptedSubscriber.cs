using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Order.Domain.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Orders.Application.Subscribers;

public class PaymentAcceptedSubscriber : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IModel _channel;
    private const string Queue = "order-service/payment-accepted";
    private const string Exchange = "order-service";
    private const string RoutingKey = "payment-accepted";

    public PaymentAcceptedSubscriber(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        var connectionFactory = new ConnectionFactory
        {
            Uri = new Uri(configuration["RabbitMq:Host"]!)
        };

        var connection = connectionFactory.CreateConnection("order-service-payment-accepted-subscriber");
        _channel = connection.CreateModel();

        _channel.ExchangeDeclare(Exchange, ExchangeType.Topic, durable: true);
        _channel.QueueDeclare(Queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueBind(Queue, Exchange, RoutingKey, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (sender, eventArgs) =>
        {
            try
            {
                var byteArray = eventArgs.Body.ToArray();
                var messageString = Encoding.UTF8.GetString(byteArray);
                var message = JsonConvert.DeserializeObject<PaymentAccepted>(messageString);

                Console.WriteLine($"Message paymentAccept received with Id: {message?.Id}");

                var result = await UpdateOrder(message!);

                if (result)
                    _channel.BasicAck(eventArgs.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                // Log and handle the exception
                Console.WriteLine($"Error processing message: {ex.Message}");
                _channel.BasicNack(eventArgs.DeliveryTag, false, true);
            }
        };

        _channel.BasicConsume(Queue, false, consumer);

        return Task.CompletedTask;
    }

    private async Task<bool> UpdateOrder(PaymentAccepted paymentAccepted)
    {
        using var scope = _serviceProvider.CreateScope();
        var orderRepository = scope.ServiceProvider.GetService<IOrderRepository>();

        if (orderRepository == null)
        {
            // Log and handle the error if repository is not available
            Console.WriteLine("Order repository is not available.");
            return false;
        }

        var order = await orderRepository.FindByIdAsync(paymentAccepted.Id);

        order.SetAsCompleted();
        await orderRepository.UpdateAsync(order);
        return true;
    }
}

public class PaymentAccepted
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
}