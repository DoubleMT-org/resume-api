using Mapster;
using Microsoft.EntityFrameworkCore;
using Resume.Data.IRepositories;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.Attachments;
using Resume.Domain.Entities.SocialMedias;
using Resume.Domain.Enums;
using Resume.Service.DTOs.AttachmentDTOs;
using Resume.Service.DTOs.SocialMediaDTOs;
using Resume.Service.Exceptions;
using Resume.Service.Extentions;
using Resume.Service.Interfaces;
using System.Linq.Expressions;
using State = Resume.Domain.Enums.EntityState;

namespace Resume.Service.Services;
public class SocialMediaService : ISocialMediaService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IAttachmentService attachmentService;

    public SocialMediaService(IUnitOfWork unitOfWork, IAttachmentService attachmentService)
    {
        this.unitOfWork = unitOfWork;
        this.attachmentService = attachmentService;
    }

    public async ValueTask<SocialMedia> CreateAsync(SocialMediaForCreationDto socialMedia)
    {
        SocialMedia existSocialMedia = await unitOfWork.SocialMedias.GetAsync(
            sm => sm.Name == socialMedia.Name
            || sm.Url == socialMedia.Url
            && sm.UserId == socialMedia.UserId
            && sm.State == State.Deleted);

        if (existSocialMedia is not null)
            throw new EventException(400, "This social media is already exists.");

        var mappedSocialMedia = socialMedia.Adapt<SocialMedia>();
        mappedSocialMedia.Create();

        var newSocialMedia = await unitOfWork.SocialMedias.CreateAsync(mappedSocialMedia);
        await unitOfWork.SaveChangesAsync();

        return newSocialMedia;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<SocialMedia, bool>> expression)
    {
        SocialMedia existSocialMedia = await unitOfWork.SocialMedias.GetAsync(expression);

        if (existSocialMedia is null || existSocialMedia.State == State.Deleted)
            throw new EventException(404, "This social media not found.");

        existSocialMedia.Delete();
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async ValueTask<IEnumerable<SocialMedia>> GetAllAsync
        (PagenationParams @params,
        Expression<Func<SocialMedia, bool>> expression = null)
    {
        return await unitOfWork.SocialMedias.GetAll(expression, false)
                                            .ToPagedList(@params)
                                            .ToListAsync();
    }

    public async ValueTask<SocialMedia> GetAsync(Expression<Func<SocialMedia, bool>> expression)
    {
        var existSocialMedia = await unitOfWork.SocialMedias.GetAsync(expression);

        if (existSocialMedia is null || existSocialMedia.State == State.Deleted)
            throw new EventException(404, "Social media not found.");

        return existSocialMedia;
    }

    public async ValueTask<SocialMedia> UpdateAsync(long id, SocialMediaForUpdateDto socialMedia)
    {
        SocialMedia existSocialMedia = await unitOfWork.SocialMedias.GetAsync(
            sm => sm.Id == id
            && sm.State != State.Deleted);

        if (existSocialMedia is not null)
            throw new EventException(404, "Social media not found");

        SocialMedia checkedSocialMedia = await unitOfWork.SocialMedias.GetAsync(
            sm => sm.Name == socialMedia.Name
            || sm.Url == socialMedia.Url
            && sm.UserId == existSocialMedia.UserId
            && sm.State != State.Deleted);

        if (checkedSocialMedia is not null)
            throw new EventException(400, "This social media informations already exist");

        var mappedSocialMedia = socialMedia.Adapt(existSocialMedia);
        mappedSocialMedia.Update();

        unitOfWork.SocialMedias.Update(mappedSocialMedia);
        await unitOfWork.SaveChangesAsync();

        return existSocialMedia;

    }

    public async ValueTask<SocialMedia> UpploadAsync(long id, AttachmentForCreationDto dto)
    {
        SocialMedia existSocialMedia = await unitOfWork.SocialMedias.GetAsync(
            sm => sm.Id == id
            && sm.State != State.Deleted);

        if (existSocialMedia is null)
            throw new EventException(400, "This social media not found!");

        Attachment checkingAttachment = await unitOfWork.Attachments.GetAsync(
            a => a.Id == existSocialMedia.AttachmentId
            && a.Type == AttachmentReference.SocialMedia);

        if (checkingAttachment is not null)
            await attachmentService.DeleteAsync(checkingAttachment.Path);

        Attachment attachment = await attachmentService.CreateAsync(
            dto, id, AttachmentReference.SocialMedia);

        existSocialMedia.Attachment = attachment;

        return existSocialMedia;
    }

}
