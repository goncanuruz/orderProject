using OrderProject.Application.Middlewares;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using OrderProject.Application.Consts;
using System.Text;
using OrderProject.Application.Extensions;
using OrderProject.Infrastructure.Services;
using Newtonsoft.Json;
using OrderProject.Application.DTOs;
using OrderProject.Application.Abstractions.Services;

namespace OrderProject.WebAPI.Consumers
{
    public class EmailConsumer : IHostedService
    {

        private readonly IConnection _connection;
        private IModel? _channel;
        private readonly string _consumerName = ConsumerConsts.EmailConsumer;
        private readonly ILogger<SerilogMiddleware> Log;
        private readonly IServiceProvider _serviceProvider;
        public EmailConsumer(IConnection connection, ILogger<SerilogMiddleware> log, IServiceProvider serviceProvider)
        {
            _connection = connection;
            Log = log;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Log.Log(LogLevel.Information, $"{_consumerName} starting");
            try
            {
                _channel = _connection.CreateModel();
                _channel.BasicQos(0, 1, false);
                _channel.QueueDeclare(queue: _consumerName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += async (model, ea) =>
                {
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    Log.Log(LogLevel.Information, $"{_consumerName} message: {message}");

                    SendEmailQuequeDto emailData = JsonConvert.DeserializeObject<SendEmailQuequeDto>(message);

                    using (IServiceScope scope = _serviceProvider.CreateScope())
                    {
                        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                        await emailService.SendEmail(emailData.Email,emailData.Content,emailData.Subject);
                    }

                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };
                _channel.BasicConsume(queue: _consumerName, autoAck: false, consumer: consumer);
                Log.Log(LogLevel.Information, $"{_consumerName} queque deleted");
            }
            catch (Exception er)
            {
                Log.Log(LogLevel.Error, $"{_consumerName} ERROR : {er.Message.ToString()}");
                Log.Log(LogLevel.Error, $"{_consumerName} ERROR JSON: {er.ToJson()}");
                throw;
            }

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel?.Close();
            _connection.Close();
            return Task.CompletedTask;
        }
    }
}
