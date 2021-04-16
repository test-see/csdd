using foundation.config;
using Mediator.Net;
using Mediator.Net.Contracts;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

namespace foundation.mediator
{
    public static class MediatorExtension
    {
        public static async Task<R> RequestPipeAsync<T, R>(this IMediator mediator, T t) where R : IResponse
        {
            var request = new PipeRequest<T>(t);
            return await mediator.RequestAsync<PipeRequest<T>, R>(request);
        }
        public static async Task SendPipeAsync<T>(this IMediator mediator, T t)
        {
            var request = new PipeCommand<T>(t);
            await mediator.SendAsync(request);
        }
        public static async Task SendStorageAsync<T>(this IMediator mediator, T t)
        {
            var request = new StorageCommand<T>(t);
            await mediator.SendAsync(request);
        }

        public static async Task<IList<R>> RequestListAsync<T, R>(this IMediator mediator, T t = null) where T : class, IRequest
        {
            var response = await mediator.RequestAsync<T, ListResponse<R>>(t);
            return response.Payloads;
        }
        public static async Task<R> RequestSingleAsync<T, R>(this IMediator mediator, T t = null) where T : class, IRequest
        {
            var response = await mediator.RequestAsync<T, ListResponse<R>>(t);
            return response.Payloads.FirstOrDefault();
        }
        public static async Task<IList<R>> RequestListByIdsAsync<T, R>(this IMediator mediator, IList<int> ids) where T : GetRequest, new()
        {
            var response = await mediator.RequestAsync<T, ListResponse<R>>(new T { Ids = ids.ToArray() });
            return response.Payloads;
        }
        public static async Task<PagerResult<R>> RequestPagerListAsync<T, R>(this IMediator mediator, PagerQuery<T> t)
        {
            return await mediator.RequestAsync<PagerQuery<T>, PagerResult<R>>(t);
        }
    }
}
