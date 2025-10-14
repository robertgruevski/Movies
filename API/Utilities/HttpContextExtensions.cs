using Microsoft.EntityFrameworkCore;

namespace API.Utilities;

public static class HttpContextExtensions
{
    public async static Task InsertPaginationParametersInHeader<T>(this HttpContext httpContext, IQueryable<T> queryable)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        double count = await queryable.CountAsync();
        httpContext.Response.Headers.Append("total-records-count", count.ToString());
    }
}
