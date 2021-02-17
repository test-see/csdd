using irespository.client.profile.model;
using irespository.user.profile.model;

namespace domain.user.valuemodel
{
    public class LoginClientValueModel
    {
        public int Id { get; set; }
        public int AuthorizeRoleId { get; set; }
        public string Name { get; set; }
        public ClientValueModel Client { get; set; }
        public UserValueModel User { get; set; }
    }
}
