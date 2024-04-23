using System.Net;

namespace TastyTrails.API.Models
{
    public record ErrorDetailResponse
    {
        public HttpStatusCode StatusCode { get; }
        public IDictionary<string, string[]>? Errors { get; }

        public ErrorDetailResponse(HttpStatusCode statusCode, IDictionary<string, string[]>? errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
