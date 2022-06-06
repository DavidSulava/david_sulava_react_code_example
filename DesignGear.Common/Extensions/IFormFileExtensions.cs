using Microsoft.AspNetCore.Http;

namespace DesignGear.Common.Extensions
{
    public static class IFormFileExtensions
    {
        public static byte[] ToArray(this IFormFile formFile)
        {
            using (var ms = new MemoryStream())
            {
                formFile.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
