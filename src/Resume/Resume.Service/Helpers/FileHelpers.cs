using Resume.Domain.Enums;
using Resume.Service.DTOs.AttachmentDTOs;

namespace Resume.Service.Helpers;
public class FileHelpers
{
    private readonly string webRootPath;

    public async Task<(string name, string path)> SaveAsync(
        AttachmentForCreationDto attachment,
        AttachmentReference destination)
    {
        // taking image full path
        string name = Guid.NewGuid().ToString("N") + Path.GetExtension(attachment.FileName);

        string lastFolderName = "";
        if (destination == AttachmentReference.SocialMedia)
            lastFolderName = "socialmedias";
        else if (destination == AttachmentReference.Comppany)
            lastFolderName = "companies";
        else if (destination == AttachmentReference.Important)
            lastFolderName = "importants";
        
        string filePath = Path.Combine(webRootPath, lastFolderName, name);

        FileStream fileStream = File.Create(filePath);
        await attachment.Data.CopyToAsync(fileStream);

        await fileStream.FlushAsync();
        fileStream.Close();

        return (name, Path.Combine(lastFolderName, name));
    }

    public Task<bool> UnsaveAsync(string path)
    {
        string fullPath = Path.Combine(webRootPath, path);

        if (!File.Exists(fullPath))
            return Task.FromResult(false);

        File.Delete(fullPath);
        return Task.FromResult(true);
    }
}
