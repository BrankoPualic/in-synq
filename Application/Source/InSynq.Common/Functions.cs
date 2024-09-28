using InSynq.Common.Extensions;
using Microsoft.AspNetCore.Http;

namespace InSynq.Common;

public static class Functions
{
    public static bool IsValidDate(DateTime date) => date >= Constants.MINIMUM_DATETIME;

    public static bool IsValidImage(IFormFile image) => Path.GetExtension(image.FileName).ToLowerInvariant().In(Constants.FILE_IMAGE_EXTENSIONS);
}