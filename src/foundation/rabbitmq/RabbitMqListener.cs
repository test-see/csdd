using RabbitMQ.Client;
using System.Threading;
using System.Threading.Tasks;

namespace foundation.rabbitmq
{
    public abstract class RabbitMqListener
    {
        protected IConnection _connection;
        protected IModel _channel;
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //if (_rabbitConnectOptions == null) return Task.CompletedTask;
            //var factory = new ConnectionFactory()
            //{
            //    HostName = _rabbitConnectOptions.HostName,
            //    Port = _rabbitConnectOptions.Port,
            //    UserName = _rabbitConnectOptions.UserName,
            //    Password = _rabbitConnectOptions.Password,
            //};
            //_connection = factory.CreateConnection();
            //_channel = _connection.CreateModel();
            //_rabbitConnectOptions.Connection = _connection;
            //_rabbitConnectOptions.Channel = _channel;
            //Process();
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (_connection != null)
                this._connection.Close();
            return Task.CompletedTask;
        }
        protected abstract void Process();
    }
}
