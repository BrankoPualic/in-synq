using Microsoft.AspNetCore.Http;

namespace InSynq.Test;

public static class PhotoHelper
{
    public static FormFile ConvertPhoto(string name)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "InSynq.Common", "Resources", "Images", name);

        var fileStream = File.OpenRead(path);
        return new FormFile(fileStream, 0, fileStream.Length, "file", Path.GetFileName(path))
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/jpg"
        };
    }
}