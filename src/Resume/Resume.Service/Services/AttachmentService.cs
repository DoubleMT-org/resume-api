using Resume.Data.IRepositories;
using Resume.Domain.Entities.Attachments;
using Resume.Domain.Enums;
using Resume.Service.DTOs.AttachmentDTOs;
using Resume.Service.Helpers;
using Resume.Service.Interfaces;

namespace Resume.Service.Services;
public class AttachmentService : IAttachmentService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly FileHelper fileHelpers;

    public AttachmentService(IUnitOfWork unitOfWork, FileHelper fileHelpers)
    {
        this.unitOfWork = unitOfWork;
        this.fileHelpers = fileHelpers;
    }

    public async ValueTask<Attachment> CreateAsync(
        AttachmentForCreationDto attachment,
        long Id,
        AttachmentReference destinationImage)
    {
        var rawAttachment = await fileHelpers.SaveAsync(attachment, destinationImage);

        var readyAttachment = new Attachment()
        {
            Name = rawAttachment.name,
            Path = rawAttachment.path,
            EntityId = Id
        };

        var newAttachment = await unitOfWork.Attachments.CreateAsync(readyAttachment);
        await unitOfWork.SaveChangesAsync();


        return newAttachment;

    }

    public async ValueTask<bool> DeleteAsync(string filePath)
    {
        await unitOfWork.Attachments.DeleteAsync( attachment => attachment.Path == filePath);

        await unitOfWork.SaveChangesAsync();

        return await fileHelpers.UnsaveAsync(filePath);
    }
    
}

