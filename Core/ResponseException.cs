using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SolidMatrix.Affair.Api.Core;

public class ResponseException : Exception
{
    public ResponseException(ExceptionCode errorCode, string message) =>
        (ErrorCode, Message) = (errorCode, message);

    public ResponseException(ExceptionCode errorCode) =>
        (ErrorCode, Message) = (errorCode, "");

    public ResponseException(ModelStateDictionary modelState)
    {
        ErrorCode = ExceptionCode.ModelInValid;
        string _message = "";
        modelState.Values.ToList().ForEach(v => v.Errors.ToList().ForEach(e => _message = string.Concat(Message, e.ErrorMessage)));
        Message = _message;
    }

    public ExceptionCode ErrorCode { get; }

    public override string Message { get; }
}