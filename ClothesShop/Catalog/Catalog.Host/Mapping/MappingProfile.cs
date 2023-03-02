using AutoMapper;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Item, ItemDto>()
               .ForMember("PictureUrl", opt
                   => opt.MapFrom<CatalogItemPictureResolver, string>(c => c.PictureFileName));
        }
    }
}
