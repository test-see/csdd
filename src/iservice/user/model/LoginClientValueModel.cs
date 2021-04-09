using irespository.user.profile.model;
using nouns.client.profile;

namespace domain.user.valuemodel
{
    public class LoginClientValueModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GetClientResponse Client { get; set; }
        public UserValueModel User { get; set; }
    }
}
