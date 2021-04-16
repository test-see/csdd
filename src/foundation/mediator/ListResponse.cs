using Mediator.Net.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace foundation.mediator
{
    public class ListResponse<T> : IResponse
    {
        public IList<T> Payloads { get; set; }
        public ListResponse(params T[] payloads)
        {
            Payloads = payloads.ToList();
        }
        public ListResponse(IList<T> payloads)
        {
            Payloads = payloads;
        }
    }
    public static class ListExtension
    {
        public static ListResponse<T> ToResponse<T>(this IList<T> payloads) where T : class
        {
            return new ListResponse<T>(payloads);
        }
    }
}
