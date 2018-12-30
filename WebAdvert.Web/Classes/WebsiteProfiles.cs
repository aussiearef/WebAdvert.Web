using AutoMapper;
using WebAdvert.Web.Models.AdvertManagement;
using WebAdvert.Web.ServiceClients;

namespace WebAdvert.Web.Classes
{
    public class WebsiteProfiles : Profile
    {
        public WebsiteProfiles()
        {
            CreateMap<CreateAdvertViewModel, CreateAdvertModel>().ReverseMap();
        }
    }
}
