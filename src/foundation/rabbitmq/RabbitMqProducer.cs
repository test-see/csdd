using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;

namespace foundation.rabbitmq
{
    public class RabbitMqProducer
    {
        private readonly RabbitMqContext _context;
        public RabbitMqProducer(RabbitMqContext context)
        {
            _context = context;
        }
        public void Publish(string queue, string message)
        {
            var channel = _context.GetChannel();
            channel.QueueDeclare(queue, true, false, false, new Dictionary<string, object>());
            var buffer = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish("", queue, null, buffer);
            channel.Close();
        }
    }
}
