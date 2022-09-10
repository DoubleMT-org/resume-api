using Mapster;
using Microsoft.EntityFrameworkCore;
using Resume.Data.IRepositories;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.SocialMedias;
using Resume.Service.DTOs.SocialMediaDTOs;
using Resume.Service.Exceptions;
using Resume.Service.Extentions;
using Resume.Service.Interfaces;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace Resume.Service.Services;
public class SocialMediaService : ISocialMediaService
{
    private readonly IUnitOfWork unitOfWork;

    public SocialMediaService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<SocialMedia> CreateAsync(SocialMediaForCreationDto socialMedia)
    {
        SocialMedia existSocialMedia = await unitOfWork.SocialMedias
                                       .GetAsync(sm => sm.Name == socialMedia.Name
                                       || sm.Url == socialMedia.Url);

        if (existSocialMedia is not null)
            throw new EventException(400, "this social media is already exists");

        var mappedSocialMedia = socialMedia.Adapt<SocialMedia>();
        mappedSocialMedia.Create();

        var newSocialMedia = await unitOfWork.SocialMedias.CreateAsync(mappedSocialMedia);
        await unitOfWork.SaveChangesAsync();

        return existSocialMedia;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<SocialMedia, bool>> expression)
    {
        SocialMedia existSocialMedia = await unitOfWork.SocialMedias.GetAsync(expression);

        if (existSocialMedia is null)
            throw new EventException(404, "This social media not found.");

        existSocialMedia.Delete();
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async ValueTask<IEnumerable<SocialMedia>> GetAllAsync
        (PagenationParams @params, Expression<Func<SocialMedia, bool>> expression = null)
    {
        return await unitOfWork.SocialMedias.GetAll(expression, false)
                                            .ToPagedList(@params)
                                            .ToListAsync();
    }

    public async ValueTask<SocialMedia> GetAsync(Expression<Func<SocialMedia, bool>> expression)
    {
        var existSocialMedia = await unitOfWork.SocialMedias.GetAsync(expression);

        if (existSocialMedia is null)
            throw new EventException(404, "Social media not found");

        return existSocialMedia;
    }

    public async ValueTask<SocialMedia> UpdateAsync(long id, SocialMediaForCreationDto socialMedia)
    {
        SocialMedia existSocialMedia = await unitOfWork.SocialMedias.GetAsync(sm => sm.Id == id
                                                                              && sm.State != Domain.Enums.EntityState.Deleted );

        if (existSocialMedia is not null)
            throw new EventException(404, "Social media not found");

        SocialMedia checkedSocialMedia = await unitOfWork.SocialMedias.GetAsync(sm => sm.Name == socialMedia.Name
                                                                                || sm.Url == socialMedia.Url 
                                                                                && sm.State != Domain.Enums.EntityState.Deleted);

        if (checkedSocialMedia is not null)
            throw new EventException(400, "This social media informations already exist");

        var mappedSocialMedia = socialMedia.Adapt(existSocialMedia);
        mappedSocialMedia.Update();
        
        unitOfWork.SocialMedias.Update(mappedSocialMedia);
        await unitOfWork.SaveChangesAsync();

        return existSocialMedia;

    }
}
