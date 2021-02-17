using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.client;
using irespository.client.profile.model;
using irespository.user.client;
using irespository.user.client.model;
using irespository.user.profile.model;
using System;
using System.Linq;

namespace respository.user
{
    public class UserClientRespository : IUserClientRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IClientRespository _ClientRespository;
        public UserClientRespository(DefaultDbContext context,
            IClientRespository ClientRespository)
        {
            _context = context;
            _ClientRespository = ClientRespository;
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

        public PagerResult<UserClientListApiModel> GetPagerList(PagerQuery<UserClientListQueryModel> query)
        {
            var sql = from r in _context.UserClient
                      join u in _context.User on r.CreateUserId equals u.Id
                      join s in _context.User on r.UserId equals s.Id
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
                          Client = new ClientValueModel
                          {
                              Id = r.ClientId,
                          }
                      };
            var data = new PagerResult<UserClientListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                foreach (var m in data.Result)
                {
                    m.Client = _ClientRespository.GetValue(m.Client.Id);
                }
            }
            return data;
        }


        public UserClientIndexApiModel GetIndexByUserId(int userId)
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
                          Client = new ClientValueModel
                          {
                              Id = r.ClientId,
                          }
                      };
            var user = sql.FirstOrDefault();
            if (user != null)
            {
                user.Client = _ClientRespository.GetValue(user.Client.Id);
            }
            return user;
        }
    }
}
