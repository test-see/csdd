using AutoMapper;
using domain.sys.entities;
using irespository.user;
using System.Collections.Generic;
using System.Linq;

namespace domain.sys
{
    public class PrivilegeContext
    {
        private readonly IDataMenuRespository _dataMenuRespository;
        private readonly IMapper _mapper;
        public PrivilegeContext(IDataMenuRespository dataMenuRespository,
            IMapper mapper)
        {
            _dataMenuRespository = dataMenuRespository;
            _mapper = mapper;
        }

        public IEnumerable<MenuEntity> GetMenuList()
        {
            var data = _dataMenuRespository.GetList();
            var menus = _mapper.Map<IEnumerable<MenuEntity>>(data.Where(x => x.ParentId == 0));
            foreach(var m in menus)
            {
                m.FindChildren(menus.ToList());
            }
            return menus;
        }

    }
}
