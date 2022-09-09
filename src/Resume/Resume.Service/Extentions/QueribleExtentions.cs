using Resume.Domain.Configurations;

namespace Resume.Service.Extentions
{
    public static class QueribleExtentions
    {
        public static IQueryable<T> ToPagedList<T>(this IQueryable<T> query, PagenationParams @params)
            where T : class
        {
            if (@params is null && @params.PageIndex < 0 && @params.PageSize <= 0)
                return query;

            return query.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize);
        }
    }
}
