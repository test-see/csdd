using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Microsoft.EntityFrameworkCore;
using storage.data.carrier;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client.profile
{
    public class ListDepartmentTypeRequestHandler : IRequestHandler<ListDepartmentTypeRequest, ListResponse<IdNameValueModel>>
    {
        private readonly DefaultDbContext _context;
        public ListDepartmentTypeRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<ListResponse<IdNameValueModel>> Handle(IReceiveContext<ListDepartmentTypeRequest> context, CancellationToken cancellationToken)
        {
            var data = await  _context.DataDepartmentType.Select(x => new IdNameValueModel { Id = x.Id, Name = x.Name }).ToListAsync();
            return data.ToResponse();
        }
    }
}
