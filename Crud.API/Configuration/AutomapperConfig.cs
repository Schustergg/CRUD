using AutoMapper;
using Crud.API.ViewModels;
using Crud.Business.Entities;

namespace Crud.API.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
