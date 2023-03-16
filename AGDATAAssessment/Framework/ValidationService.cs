using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace AGDATAAssessment.Framework
{
    public static class ValidationService
    {
        public static void Validate(ModelStateDictionary modelState)
        {
            if(!modelState.IsValid)
            {
                List<string> errors = new List<string>();

                foreach(var value in modelState.Values)
                {
                    foreach(var error in value.Errors)
                    {
                        errors.Add($"<li>{error.ErrorMessage}</li>");
                    }
                }

                throw new ValidationException(400, $"<ul>{String.Join("", errors)}</ul>");
            }
        }
    }
}
