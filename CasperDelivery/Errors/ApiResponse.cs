namespace CasperDelivery.Errors;

public class ApiResponse
{
    public ApiResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }



    public int StatusCode { get; set; }
    public string Message { get; set; }
    
    private string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "Here is a bad request",
            401 => "Here is unauthorized error",
            404 => "Not Found",
            500 => "Server Error",
            _ => null
        };
    }
}