using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Microsoft.EntityFrameworkCore;
using storage.data.carrier;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client.profile
{
    public class ListDepartmentTypeStorageRequestHandler : IRequestHandler<StorageRequest<ListDepartmentTypeRequest>, ListResponse<DataDepartmentType>>
    {
        private readonly DefaultDbContext _context;
        public ListDepartmentTypeStorageRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<ListResponse<DataDepartmentType>> Handle(IReceiveContext<StorageRequest<ListDepartmentTypeRequest>> context, CancellationToken cancellationToken)
        {
            var data = await  _context.DataDepartmentType.ToListAsync();
            return new ListResponse<DataDepartmentType>(data.ToArray());
        }
    }
}
