using Storage.Models.ViewModels;

namespace Storage.Models
{
    public interface IImageRepository
    {
        Task CreateAsync(CreateImageDto image);
        Task<ImageInputViewModel?> GetImageByIdAsync(int id);
    }
}
