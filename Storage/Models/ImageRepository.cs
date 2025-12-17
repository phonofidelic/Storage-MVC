using Storage.Data;
using Storage.Models;
using Storage.Models.Entities;

internal class ImageRepository : IImageRepository
{
    private readonly StorageContext _storageContext;

    public ImageRepository(StorageContext storageContext)
    {
        _storageContext = storageContext; 
    }
    public async Task CreateAsync(CreateImageDto image)
    {
        await _storageContext.AddAsync(new Image()
        {
            Alt = image.AltText,
            Path = image.ImagePath
        });
    }

    public async Task<GetImageDto?> GetImageByIdAsync(int id)
    {
        var image = await _storageContext.FindAsync<Image>(id);

        if (image == null) {
            return null;
        }

        return new GetImageDto(Id: image.Id, AltText: image.Alt, ImagePath: image.Path);
    }
}