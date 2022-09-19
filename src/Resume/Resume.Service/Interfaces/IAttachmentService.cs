using Resume.Domain.Entities.Attachments;
using Resume.Domain.Enums;
using Resume.Service.DTOs.AttachmentDTOs;

namespace Resume.Service.Interfaces;
public interface IAttachmentService
{
    ValueTask<Attachment> CreateAsync(
        AttachmentForCreationDto attachment,
        long Id,
        AttachmentReference destinationImage);

    ValueTask<bool> DeleteAsync(string filePath);
}
