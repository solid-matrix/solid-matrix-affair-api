namespace SolidMatrix.Affair.Api;

public class UniformMessage
{
    public UniformMessage(object? data, bool success, string message)
    {
        Data = data;
        Success = success;
        Message = message;
    }

    public UniformMessage(object? data, bool success)
    {
        Data = data;
        Success = success;
        Message = "";
    }

    public UniformMessage(object? data)
    {
        Data = data;
        Success = true;
        Message = "";
    }

    public bool Success;
    public string Message;
    public object? Data;
}

public class SuccessMessage : UniformMessage
{
    public SuccessMessage(object? data) : base(data, true) { }
    public SuccessMessage(object? data, string message) : base(data, true, message) { }
}

public class ErrorMessage : UniformMessage
{
    public ErrorMessage(object? data) : base(data, false) { }
    public ErrorMessage(object? data, string message) : base(data, false, message) { }
}
