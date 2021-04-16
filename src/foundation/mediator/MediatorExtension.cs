using Mediator.Net;
using System.Threading.Tasks;

namespace foundation.mediator
{
    public static class MediatorExtension
    {
        public static async Task<ListResponse<R>> RequestStorageAsync<T,R>(this IMediator mediator, T t)
        {
            var request = new StorageRequest<T>(t);
            return await mediator.RequestAsync<StorageRequest<T>, ListResponse<R>>(request);
        }
    }
}
