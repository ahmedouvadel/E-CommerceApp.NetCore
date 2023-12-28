using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
namespace E_CommerceWebApplication.Helper;
public class ImageSave
{
    public ImageSave()
    {
        
    }
    private  readonly IHostingEnvironment _hostingEnvironment;//To get root phth


    public ImageSave(IHostEnvironment hostingEnvironment)
    {
        //_hostingEnvironment = hostingEnvironment;
    }

    public string GetUniqueNameForImage(IFormFile image)
    {
        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
        string fileName = Path.Combine(uploadsFolder, uniqueFileName);

        //StoreFileInRoot(fileName, image);
        return fileName;
    }

    private void StoreFileInRoot(string fileName, IFormFile image)
    {
        using (var fileStream = new FileStream(fileName, FileMode.Create))
        {
            image.CopyTo(fileStream);
        }
    }
}
