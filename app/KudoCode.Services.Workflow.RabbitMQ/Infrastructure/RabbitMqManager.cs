using System;
using System.Text;
using Autofac;
using KudoCode.Messaging.Infrastructure.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Core.Services.Workflow.RabbitMQ.Infrastructure
{
    public class RabbitMqManager : IDisposable, IEventManager
    {
        private static readonly object lockObject = new object();
        private readonly string _exchange;
        private readonly string _queue;
        private readonly IModel channel;

        public RabbitMqManager(string source)
        {
            var connectionFactory =
                new ConnectionFactory {Uri = new Uri(RabbitMqConstants.RabbitMqUri)};
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();

            _exchange = $"core.{source}.exchange";
            _queue = $"core.{source}.queue";

            channel.QueueDeclare(_queue, false, false, false, null);
            channel.BasicQos(0, 1, false);
        }

        public void Dispose()
        {
            if (!channel.IsClosed)
                channel.Close();
        }

        public void Send<T>(T request)
        {
            channel.ExchangeDeclare(_exchange, ExchangeType.Direct);
            channel.QueueDeclare(_queue, false, false, false, null);
            channel.QueueBind(_queue, _exchange, "");

            var serializedCommand = JsonConvert.SerializeObject(request, KudoCode.Helpers.Json.Serialization.Objects());

            var messageProperties = channel.CreateBasicProperties();
            messageProperties.ContentType =
                RabbitMqConstants.JsonMimeType;

            channel.BasicPublish(_exchange, "", messageProperties, Encoding.UTF8.GetBytes(serializedCommand));
        }

        public IEventManager Listen()
        {
            var eventingConsumer = new EventingBasicConsumer(channel);
            eventingConsumer.Received += (chan, eventArgs) =>
            {
                var contentType = eventArgs.BasicProperties.ContentType;
                if (contentType != RabbitMqConstants.JsonMimeType)
                    throw new ArgumentException(
                        $"Can't handle content type {contentType}");

                var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                var requestDto = JsonConvert.DeserializeObject(message, KudoCode.Helpers.Json.Serialization.Objects());

                ApplicationContext.Container
                    .Resolve<EventExecutionPipeLine>()
                    .Execute(requestDto);

                try
                {
                    lock (lockObject)
                        channel.BasicAck(eventArgs.DeliveryTag, true);
                }
                catch (Exception)
                {
                    throw;
                }
            };
            try
            {
                channel.BasicConsume(_queue, false, eventingConsumer);
            }
            catch (Exception)
            {
                throw;
            }

            return this;
        }
    }
}