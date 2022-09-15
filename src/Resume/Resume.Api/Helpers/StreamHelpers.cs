using Resume.Service.DTOs.AttachmentDTOs;

namespace Resume.Api.Helpers
{
    public static class StreamHelpers
    {
        public static AttachmentForCreationDto GetAsAttachment(this IFormFile formFile)
        {
            using var ms = new MemoryStream();
            formFile.CopyTo(ms);

            return new AttachmentForCreationDto()
            {
                Data = ms.ToArray(),
                FileName = formFile.FileName
            };
        }
    }
}
