namespace Common.GlobalExceptionsResponses;

public class ResponseModel
{


	public List<string> Errors { get; set; }

	public ResponseModel(List<string> messages)
	{
		Errors = messages;

	}

	public ResponseModel()
	{

		Errors = null;
	}

}
