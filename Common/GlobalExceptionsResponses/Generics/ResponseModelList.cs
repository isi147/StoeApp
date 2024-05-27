namespace Common.GlobalExceptionsResponses.Generics;

public class ResponseModelList<T> : ResponseModel<T>
{
	int _count = 0;
	List<T> _data = new();
	public int TotalCount { get { return _count; } }

	public List<T> Data
	{
		get { return _data; }
		set
		{
			_data = value;
			_count = _data.Count;
		}
	}
	public ResponseModelList(List<string> erorrs) : base(erorrs)
	{

	}
	public ResponseModelList()
	{

	}
}
