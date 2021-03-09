using foundation.config;

namespace irespository.data.model
{
    public class MenuValueModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Path { get; set; }
        public int ParentId { get; set; }
        public int Id { get; set; }
        public IdNameValueModel Portal { get; set; }
        public string Icon { get; set; }
        public bool HideInMenu { get; set; }
        public bool HideChildrenInMenu { get; set; }
    }
}
