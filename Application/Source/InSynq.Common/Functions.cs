using InSynq.Common.Extensions;
using Microsoft.AspNetCore.Http;

namespace InSynq.Common;

public static class Functions
{
    public static bool IsValidDate(DateTime date) => date >= Constants.MINIMUM_DATETIME;

    public static bool IsValidImage(IFormFile image) => Path.GetExtension(image.FileName).ToLowerInvariant().In(Constants.FILE_IMAGE_EXTENSIONS);

    public static bool AtLeast16YearsOld(DateTime dob) => dob <= DateTime.Today.AddYears(-16);

    public static bool WithinFileSize(IFormFile image) => image.Length <= Constants.FILE_SIZE_10MB;
}