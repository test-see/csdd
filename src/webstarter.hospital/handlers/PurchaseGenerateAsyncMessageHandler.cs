using foundation.config;
using iservice.purchase;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client.Core.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection.MessageHandlers;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;

namespace webstarter.hospital.handlers
{
    public class PurchaseGenerateAsyncMessageHandler : IAsyncMessageHandler
    {
        private readonly ILogger<PurchaseGenerateAsyncMessageHandler> _logger;
        private readonly IPurchaseService _service;
        public PurchaseGenerateAsyncMessageHandler(ILogger<PurchaseGenerateAsyncMessageHandler> logger,
            IPurchaseService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task Handle(BasicDeliverEventArgs eventArgs, string matchingRoute)
        {
            _logger.LogInformation($"Handling message {eventArgs.GetMessage()} by routing key {matchingRoute}");
            var purchase = JsonConvert.DeserializeObject<RabbitMqMessage<int>>(eventArgs.GetMessage());
            await _service.GenerateAsync(purchase.Payload);
            _logger.LogInformation($"Completed message {eventArgs.GetMessage()} by routing key {matchingRoute}");
        }
    }
}
