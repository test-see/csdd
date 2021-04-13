using System;

namespace irespository.client.model
{
    public class ListingClientResponse 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
    }
}
