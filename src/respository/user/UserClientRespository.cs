using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.client;
using irespository.user.client;
using irespository.user.client.model;
using irespository.user.profile.model;
using Mediator.Net;
using nouns.client.profile;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace respository.user
{
    public class UserClientRespository : IUserClientRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public UserClientRespository(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public UserClient Create(UserClientCreateApiModel created, int userId)
        {
            var user = new UserClient
            {
                Name = created.Name,
                CreateUserId = userId,
                ClientId = created.ClientId,
                UserId = created.UserId,
                CreateTime = DateTime.Now,
            };

            _context.UserClient.Add(user);
            _context.SaveChanges();

            return user;
        }

        public int Delete(int id)
        {
            var user = _context.UserClient.Find(id);
            _context.UserClient.Remove(user);
            _context.SaveChanges();
            return id;
        }

        public async Task<PagerResult<UserClientListApiModel>> GetPagerListAsync(PagerQuery<UserClientListQueryModel> query)
        {
            var sql = from r in _context.UserClient
                      join u in _context.User on r.CreateUserId equals u.Id
                      join s in _context.User on r.UserId equals s.Id
                      orderby r.Id descending
                      select new UserClientListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          CreateUserName = u.Username,
                          User = new UserValueModel
                          {
                              Id = s.Id,
                              Phone = s.Phone,
                              Username = s.Username,
                          },
                          Client = new GetClientResponse
                          {
                              Id = r.ClientId,
                          }
                      };
            if (query.Query != null && query.Query.ClientId != null)
            {
                sql = sql.Where(x => x.Client.Id == query.Query.ClientId.Value);
            }
            if (!string.IsNullOrEmpty(query.Query?.Phone))
            {
                sql = sql.Where(x => x.User.Phone.Contains(query.Query.Phone));
            }
            if (!string.IsNullOrEmpty(query.Query?.Name))
            {
                sql = sql.Where(x => x.Name.Contains(query.Query.Name));
            }
            var data = new PagerResult<UserClientListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var clients = await _mediator.ListByIdsAsync<GetClientRequest, GetClientResponse>(data.Select(x => x.Client.Id));
                foreach (var m in data.Result)
                {
                    m.Client = clients.FirstOrDefault(x => x.Id == m.Client.Id);
                }
            }
            return data;
        }


        public async Task<UserClientIndexApiModel> GetIndexByUserIdAsync(int userId)
        {
            var sql = from r in _context.UserClient
                      join u in _context.User on r.CreateUserId equals u.Id
                      join s in _context.User on r.UserId equals s.Id
                      where r.UserId == userId
                      select new UserClientIndexApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          CreateUserName = u.Username,
                          User = new UserValueModel
                          {
                              Id = s.Id,
                              Phone = s.Phone,
                              Username = s.Username,
                          },
                          Client = new GetClientResponse
                          {
                              Id = r.ClientId,
                          }
                      };
            var user = sql.FirstOrDefault();
            if (user != null)
            {
                user.Client = await _mediator.GetByIdAsync<GetClientRequest, GetClientResponse>(user.Client.Id );
            }
            return user;
        }
    }
}
