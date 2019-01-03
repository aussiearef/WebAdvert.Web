using AutoMapper;
using WebAdvert.Web.Models;
using WebAdvert.Web.Models.AdvertManagement;
using WebAdvert.Web.Models.Home;
using WebAdvert.Web.ServiceClients;

namespace WebAdvert.Web.Classes
{
    public class WebsiteProfiles : Profile
    {
        public WebsiteProfiles()
        {
            CreateMap<CreateAdvertViewModel, CreateAdvertModel>().ReverseMap();
            CreateMap<AdvertType, SearchViewModel>()
                .ForMember(
                    dest => dest.Id, src => src.MapFrom(field => field.Id))
                .ForMember(
                    dest => dest.Title, src => src.MapFrom(field => field.Title));
        }
    }
}
