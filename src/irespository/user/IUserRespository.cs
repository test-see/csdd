using foundation.ef5.poco;
using System.Threading.Tasks;

namespace irespository.user
{
    public interface IUserRespository
    {
        Task AddActiveUserAsync(string phone);
        User GetByPhone(string phone);
    }
}
