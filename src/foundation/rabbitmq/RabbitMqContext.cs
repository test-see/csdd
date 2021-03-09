using foundation.config;
using RabbitMQ.Client;

namespace foundation.rabbitmq
{
    public class RabbitMqContext
    {
        protected IConnection _connection;
        private readonly RabbitMqConfig _config;
        public RabbitMqContext(RabbitMqConfig config)
        {
            _config = config;
        }
        protected IModel GetChannel()
        {
            if (_connection == null)
            {
                var factory = new ConnectionFactory();
                factory.Port = _config.Port;
                factory.UserName = _config.UserName;
                factory.VirtualHost = _config.HostName;
                factory.Password = _config.Password;
                _connection = factory.CreateConnection();
            }
            return _connection.CreateModel();
        }
    }
}
