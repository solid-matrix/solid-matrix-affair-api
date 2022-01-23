using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SolidMatrix.Affair.Api.Core;

public class ExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception == null) return;

        context.Result = new JsonResult(context.Exception switch
        {
            // inter controller exception
            ResponseException ex => UniformResult.Error(ex.ErrorCode, ex.Message),

            // database exceptions
            UniqueConstraintException ex => UniformResult.Error(ExceptionCode.UniqueConstraint, ex.Message),
            CannotInsertNullException ex => UniformResult.Error(ExceptionCode.CannotInsertNull, ex.Message),
            MaxLengthExceededException ex => UniformResult.Error(ExceptionCode.MaxLengthExceeded, ex.Message),
            NumericOverflowException ex => UniformResult.Error(ExceptionCode.NumericOverflow, ex.Message),
            ReferenceConstraintException ex => UniformResult.Error(ExceptionCode.ReferenceConstraint, ex.Message),

            // unknown error
            _ => UniformResult.Error(ExceptionCode.Unknown, context.Exception.Message),
        });
        context.ExceptionHandled = true;
    }
}