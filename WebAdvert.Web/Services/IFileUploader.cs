using System.IO;
using System.Threading.Tasks;

namespace WebAdvert.Web.Services
{
    public interface IFileUploader
    {
        Task<bool> UploadFileAsync(string fileName, Stream storageStream);
    }
}