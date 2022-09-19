using Resume.Domain.Enums;
using Resume.Service.DTOs.AttachmentDTOs;
using WebPath = Resume.Service.Helpers.WebEnvironmentHelper;

namespace Resume.Service.Helpers;
public class FileHelper
{
    public async Task<(string name, string path)> SaveAsync(
        AttachmentForCreationDto attachment,
        AttachmentReference destination)
    {
        // taking image full path
        string name = Guid.NewGuid().ToString("N") + Path.GetExtension(attachment.FileName);
        string lastFolderName = "";
        if (destination == AttachmentReference.SocialMedia)
            lastFolderName = WebPath.SocialMediaImagesPath;
        else if (destination == AttachmentReference.Comppany)
            lastFolderName = WebPath.CompanyImagesPath;
        else if (destination == AttachmentReference.Important)
            lastFolderName = WebPath.ImportantImagesPath;

        string filePath = Path.Combine(WebPath.WebRootPath, lastFolderName, name);
        await File.WriteAllBytesAsync(filePath, attachment.Data);

        return (name, Path.Combine(lastFolderName, name));
    }

    public Task<bool> UnsaveAsync(string path)
    {
        string fullPath = Path.Combine(WebPath.WebRootPath, path);

        if (!File.Exists(fullPath))
            return Task.FromResult(false);

        File.Delete(fullPath);
        return Task.FromResult(true);
    }
}
