using System.ComponentModel.DataAnnotations;

namespace AGDATAAssessment.Framework
{
    public class ValidationException : Exception
    {
        public ValidationException(int statusCode, object? value = null) =>
        (StatusCode, Value) = (statusCode, value);

        public int StatusCode { get; }

        public object? Value { get; }
    }
}
