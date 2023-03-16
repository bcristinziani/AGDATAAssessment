using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace AGDATAAssessment.Framework
{
    public class ValidationExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ValidationException validationException)
            {
                context.Result = new ObjectResult(validationException.Value)
                {
                    StatusCode = validationException.StatusCode
                };

                context.ExceptionHandled = true;
            }
            else if(context.Exception is DbUpdateException dbUpdateException)
            {
                string errorMessage = dbUpdateException.InnerException?.Message?.Contains("SQLite Error 19") == true 
                    ? "Cannot have duplicate records" 
                    : "An error occurred while attempting to save your changes";

                context.Result = new ObjectResult(errorMessage)
                {
                    StatusCode = 400
                };

                context.ExceptionHandled = true;
            }
            else if(context.Exception != null)
            {
                context.Result = new ObjectResult("Something went wrong while trying to process your request. Please wait a few minutes and try again.")
                {
                    StatusCode = 500
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
