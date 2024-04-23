namespace TastyTrails.Application.Common.Exceptions
{
    public class RequestValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public RequestValidationException(Dictionary<string, string[]> errors) 
        {
            Errors = errors;
        }
    }
}
