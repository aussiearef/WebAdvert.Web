using System.Threading.Tasks;
using AdvertApi.Models;

namespace WebAdvert.Web.ServiceClients
{
    public interface IAdvertApiClient
    {
        Task<AdvertResponse> Create(CreateAdvertModel model);
        Task<bool> Confirm(ConfirmAdvertRequest model);
    }
}
