namespace Common.GlobalExceptionsResponses;

public class ErrorResponse
{
	public int Code { get; set; }
	public string Message { get; set; }
	public string ErrorType { get; set; }
	public ErrorResponse(string message, ErrorType errorType)
	{

		Message = message;
		ErrorType = errorType.ToString();
		Code = (int)errorType;
	}
    public ErrorResponse()
    {
        
    }

}


public enum ErrorType
{
	BAD_REQUEST = 1,
	NOT_FOUND = 2,
	VALIDATION_ERROR = 3
}