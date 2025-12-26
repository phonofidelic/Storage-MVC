using System.Runtime;
using System.Runtime.CompilerServices;
using Storage.Data;
using Storage.Models;
using Storage.Models.Entities;
using Storage.Models.ViewModels;

namespace Storage.Models;
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

    public async Task<ImageInputViewModel?> GetImageByIdAsync(int id)
    {
        var image = await _storageContext.FindAsync<Image>(id);

        if (image == null) {
            return null;
        }

        return new ImageInputViewModel() {
            Alt = image.Alt,
            Path = image.Path,
        };
    }
}