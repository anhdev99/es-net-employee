namespace EmployeesESNET.Helpers;

public class ResultResponse<T>
{
    public int Code { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public ResultResponse<T> WithCode(int code)
    {
        if (code != null)
        {
            Code = code;
        }

        return this;
    }
    public ResultResponse<T> WithMessage(string message)
    {
        if (message != null)
        {
            Message = message;
        }

        return this;
    }
    
    public ResultResponse<T> WithData(T data)
    {
        if (data != null)
        {
            Data = data;
        }

        return this;
    }
}