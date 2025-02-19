using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderProject.Application.Abstractions.Services;
using OrderProject.Application.Consts;
using OrderProject.Application.DTOs;
using OrderProject.Application.Middlewares;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Infrastructure.Services
{
    public class RabbitMqService:IRabbitMqService
    {
        private readonly string _emailConsumer =ConsumerConsts.EmailConsumer;
        private readonly IConnection _connection;
        private readonly ILogger<SerilogMiddleware> Log;

        public RabbitMqService(IConnection connection, ILogger<SerilogMiddleware> log)
        {
            _connection = connection;
            Log = log;
        }

        public void SendEmailQueque(SendEmailQuequeDto data)
        {
            SendMessage(data,_emailConsumer);
        }

        private void SendMessage(object data, string name)
        {
            var _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: name,
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var message = JsonConvert.SerializeObject(data, settings);
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "",
                                  routingKey: name,
                                  basicProperties: null,
                                  body: body);
            _channel.Close();
        }
    }
}
