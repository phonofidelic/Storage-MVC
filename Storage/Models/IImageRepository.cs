namespace Storage.Models
{
    public interface IImageRepository
    {
        Task CreateAsync(CreateImageDto image);
        Task<GetImageDto?> GetImageByIdAsync(int id);
    }
}
