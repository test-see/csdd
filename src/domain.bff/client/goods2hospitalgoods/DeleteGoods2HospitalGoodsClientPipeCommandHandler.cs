using domain.client;
using domain.client.goods2hospitalgoods.entity;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteClientGoods2HospitalGoodsPipeCommandHandler : ICommandHandler<Pipe<DeleteClientGoods2HospitalGoodsCommand>>
    {
        private readonly ClientGoods2HospitalGoodsService _clientContext;
        public DeleteClientGoods2HospitalGoodsPipeCommandHandler(ClientGoods2HospitalGoodsService clientContext)
        {
            _clientContext = clientContext;
        }
        public async Task Handle(IReceiveContext<Pipe<DeleteClientGoods2HospitalGoodsCommand>> context, CancellationToken cancellationToken)
        {
            await _clientContext.DeleteAsync(context.Message.Payload);
        }
    }
}
