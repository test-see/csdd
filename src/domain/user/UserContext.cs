using foundation.config;
using foundation.ef5.poco;
using irespository.user;
using irespository.user.model;

namespace domain.user
{
    public class UserContext
    {
        private readonly IUserRespository _userRespository;
        public UserContext(IUserRespository userRespository)
        {
            _userRespository = userRespository;
        }
        public PagerResult<User> GetPagerList(PagerQuery<UserListQueryModel> query)
        {
            return _userRespository.GetPagerList(query);
        }

    }
}
