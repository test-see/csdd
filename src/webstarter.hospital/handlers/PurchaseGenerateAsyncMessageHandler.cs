using DotNetCore.CAP;
using foundation.config;
using iservice.purchase;
using System.Threading.Tasks;

namespace webstarter.hospital.handlers
{
    public class PurchaseGenerateAsyncMessageHandler: ICapSubscribe
    {
        private readonly IPurchaseService _service;
        public PurchaseGenerateAsyncMessageHandler(IPurchaseService service)
        {
            _service = service;
        }
        [CapSubscribe("purchase.generate")]
        public async Task Handle(RabbitMqMessage<int> message)
        {
            await _service.GenerateAsync(message.Payload);
        }
    }
}
