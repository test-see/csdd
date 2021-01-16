using foundation.config;
using System;
using System.Collections.Generic;

namespace irespository.client.model
{
    public class ClientIndexApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public IList<int> HospitalClientIds { get; set; }
    }
}
