namespace Common.GlobalExceptionsResponses.Generics;

public class ResponseModel<T>
{
	public T? Data { get; set; }

	public ResponseModel(T data)
	{
		Data = data;
	}
}