using AutoMapper;
using domain.sys.entities;
using foundation.ef5.poco;

namespace domain.sys.mappers
{
    public class MenuMapper : Profile
    {
        public MenuMapper()
        {
            CreateMap<DataMenu, MenuEntity>();
        }
    }
}
