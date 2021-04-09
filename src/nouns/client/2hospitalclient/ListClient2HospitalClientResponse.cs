using irespository.hospital.client.model;
using Mediator.Net.Contracts;
using nouns.client.profile;
using System;

namespace irespository.client.maping.model
{
    public class ListClient2HospitalClientResponse : IResponse
    {
        public int ClientMappingId { get; set; }
        public GetHospitalClientResponse HospitalClient { get; set; }
        public GetClientResponse Client { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
    }
}
