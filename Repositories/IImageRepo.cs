using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Project.Repositories
{
    public interface IImageRepo
    {
        bool DeleteImage(string imageName);
        Task<bool> StoreImage(string folderName,string imageName, IFormFile image);
    }
}