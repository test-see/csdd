using foundation.config;
using Mediator.Net;
using Mediator.Net.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foundation.mediator
{
    public static class MediatorExtension
    {
        public static async Task<R> ToPipeAsync<T, R>(this IMediator mediator, T t) where R : IResponse
        {
            var request = new Pipe<T>(t);
            return await mediator.RequestAsync<Pipe<T>, R>(request);
        }
        public static async Task ToPipeAsync<T>(this IMediator mediator, T t) 
        {
            var request = new Pipe<T>(t);
            await mediator.SendAsync(request);
        }
        public static async Task<IList<R>> ListAsync<T, R>(this IMediator mediator, T t = null) where T : class, IRequest
        {
            var response = await mediator.RequestAsync<T, ListResponse<R>>(t);
            return response.Payloads;
        }
        public static async Task<R> GetAsync<T, R>(this IMediator mediator, T t = null) where T : class, IRequest
        {
            var response = await mediator.RequestAsync<T, ListResponse<R>>(t);
            return response.Payloads.FirstOrDefault();
        }
        public static async Task<R> GetByIdAsync<T, R>(this IMediator mediator, int id) where T : GetRequest, new()
        {
            var response = await mediator.RequestAsync<T, ListResponse<R>>(new T { Ids = new int[] { id } });
            return response.Payloads.FirstOrDefault();
        }
        public static async Task<IList<R>> ListByIdsAsync<T, R>(this IMediator mediator, IList<int> ids) where T : GetRequest, new()
        {
            var response = await mediator.RequestAsync<T, ListResponse<R>>(new T { Ids = ids.ToArray() });
            return response.Payloads;
        }
        public static async Task<PagerResult<R>> ListByPageAsync<T, R>(this IMediator mediator, PagerQuery<T> t)
        {
            return await mediator.RequestAsync<PagerQuery<T>, PagerResult<R>>(t);
        }
    }
}
