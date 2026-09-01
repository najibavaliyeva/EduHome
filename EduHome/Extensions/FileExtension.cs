using EduHome.Contexts;
using EduHome.Enums;
using EduHome.Services.Interfaces;
using EduHome.ViewModels.Slider;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Extensions
{
    public  static class FileExtension
    {
      
            
            public static string UploadFile( this IFormFile file, string root, string path)
            {
                var fileName = $"{Guid.NewGuid()}{file.FileName}";
                var fullPath = Path.Combine(root, path, fileName);
                using var stream = new FileStream(fullPath, FileMode.Create);
                file.CopyTo(stream);
                return fileName;
            }
           public static  bool IsSizeValid( this IFormFile file, int size, FileSize filesize)
            {
                switch (filesize)
                {
                    case FileSize.Byte:
                        if (file.Length > (int)size) return false;
                        break;
                    case FileSize.KB:
                        if (file.Length > (int)size * 1024) return false;
                        break;
                    case FileSize.MB:
                        if (file.Length > (int)size * 1024 * 1024) return false;
                        break;
                    case FileSize.GB:
                        if (file.Length > (int)size * 1024 * 1024 * 1024) return false;
                        break;
                    default: return false;
                }
                return true;
            }
             public static bool IsFormatValid( this IFormFile file)
            {
                return file.ContentType.Contains("image/") ? true : false;
            }
        }
}
