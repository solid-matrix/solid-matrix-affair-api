namespace SolidMatrix.Affair.Api.Core;

public class UniformResult
{
    public bool Success;
    public int ErrorCode;
    public string Message = null!;
    public object? Data;

    public static int DefaultErrorCode = 1;
    public static UniformResult Ok(object? data) => new() { Success = true, ErrorCode = 0, Message = "", Data = data };
    public static UniformResult Ok(object? data, string message) => new() { Success = true, ErrorCode = 0, Message = message, Data = data };
    public static UniformResult Error(int errorCode, string message) => new() { Success = false, ErrorCode = errorCode, Message = message, Data = null };
    public static UniformResult Error(int errorCode) => new() { Success = false, ErrorCode = errorCode, Message = "", Data = null };
    public static UniformResult Error(string message) => new() { Success = false, ErrorCode = DefaultErrorCode, Message = message, Data = null };
}
