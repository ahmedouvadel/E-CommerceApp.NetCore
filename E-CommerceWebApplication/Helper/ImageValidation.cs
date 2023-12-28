namespace E_CommerceWebApplication.Helper;
public class ImageValidation
{
    private static readonly int _maxAllowedPosterSize = 1048576*2;
    private static readonly List<string> _allowedExtensions = new() { ".jpg", ".png" };

    public static bool IsSizeValid(IFormFile image)
        =>  (image.Length <= _maxAllowedPosterSize);

    public static bool IsValidExtensions(IFormFile image)
        => _allowedExtensions.Contains(Path.GetExtension(image.FileName.ToLower()));
    
}
