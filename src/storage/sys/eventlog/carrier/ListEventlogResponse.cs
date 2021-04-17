using System;

namespace irespository.sys.model
{
    public class ListEventlogResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public string OptionUsername { get; set; }
    }
}
