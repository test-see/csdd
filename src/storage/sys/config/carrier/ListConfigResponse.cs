using System;

namespace irespository.sys.model
{
    public class ListConfigResponse
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
    }
}
