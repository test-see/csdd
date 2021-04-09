using irespository.client.maping.model;
using Mediator.Net.Contracts;
using System;
using System.Collections.Generic;

namespace nouns.client.profile
{
    public class GetClientResponse: IResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public IList<ListClient2HospitalClientResponse> HospitalClients { get; set; }
    }
}
