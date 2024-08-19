namespace credo_bank.Application.Utilities.ApiServiceResponse;

public class ApiWrapper<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public static ApiWrapper<T> SuccessResponse(T data, string? message = null)
    {
        return new ApiWrapper<T> { Success = true, Message = message, Data = data };
    }

    public static ApiWrapper<T> FailureResponse(string? message = null)
    {
        return new ApiWrapper<T> { Success = false, Message = message };
    }
}