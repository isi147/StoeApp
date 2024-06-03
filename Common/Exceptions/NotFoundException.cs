namespace Common.Exceptions;

public class NotFoundException : Exception
{
	public const string Message = "Object not found";
	public NotFoundException(string message = Message) : base(message)
	{

	}

}
