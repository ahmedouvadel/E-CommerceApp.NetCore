namespace E_CommerceWebApplication.Helper;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
public class ImageResizer
{
    public static void Resize(string sourcePath, string outputPath, int width, int height)
    {
        using (Image image = Image.Load(sourcePath))
        {
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(width, height),
                Mode = ResizeMode.Max
            }));

            image.Save(outputPath); // Save the resized image to the output path
        }
    }


}
