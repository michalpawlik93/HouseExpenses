using HousExpenses.Domain.Exceptions;
using System.Net;

namespace HouseExpenses.Api.Utils
{
    public static class HttpUtils
    {
        public static HttpStatusCode GetStatusCode(Exception exception)
        {
            return exception switch
            {
                ResourceNotFoundException _ => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };
        }
    }
}
